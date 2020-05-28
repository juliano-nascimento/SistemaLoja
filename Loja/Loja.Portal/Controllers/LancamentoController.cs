using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jarvis.Utils;
using Loja.Business.Interfaces;
using Loja.Repository.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Loja.Portal.Controllers
{
    [Authorize]
    public class LancamentoController : Controller
    {
        private readonly ILancamentoBusiness _business;       
        public LancamentoController(ILancamentoBusiness business)
        {
            _business = business;            
        }
        public async Task<IActionResult> Index()
        {
            LancamentoDto model = new LancamentoDto();
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            try
            {

                model.ListaLancamento = await _business.ObterLancamentos();
                return View(model);

            }
            catch(Exception ex)
            {
                HttpContext.Session.SetString("error", ex.Message);
                return View(model);
            }
           
            
        }

        public async Task<IActionResult> Adicionar(LancamentoDto lancamento)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            try
            {
                lancamento.ListaContas = await _business.ObterContas();
                return View(lancamento);
            }
            catch(Exception ex)
            {
                HttpContext.Session.SetString("error", ex.Message);
                return View(lancamento);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Adiciona(LancamentoDto lancamento)
        {
            if (ModelState.IsValid)
            {
                lancamento.UsuarioId= Convert.ToInt32(User.Claims.Where(a => a.Type.Equals("IdUsuario")).First().Value);
                try
                {
                    if (await _business.Create(lancamento))
                    {
                        HttpContext.Session.SetString("success", "Lancamento adicionado com sucesso!");
                        return RedirectToAction("Adicionar");
                    }
                    else
                        HttpContext.Session.SetString("error", "Erro ao adicionar lancamento. ");

                    return RedirectToAction("Adicionar", lancamento);

                }
                catch(Exception ex)
                {
                    HttpContext.Session.SetString("error", ex.Message);
                    return RedirectToAction("Adicionar", lancamento);
                }
                
            }
            else
            {
                HttpContext.Session.SetString("error", "Verifique os dados preenchidos! ");
            }

            return RedirectToAction("Adicionar", lancamento);
        }

        public async Task<IActionResult> Visualizar()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            LancamentoDto model;
            string[] uri = HttpContext.Request.Path.ToUriComponent().Split("/");
            int Id = Encrypt.DecryptId(uri[3]);
            model = await _business.ObterLancamentoPorId(Id);
            return View(model);
        }
                
        public async Task<IActionResult> Editar(LancamentoDto lancamento)
        {          
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            string[] uri = HttpContext.Request.Path.ToUriComponent().Split("/");
            int Id = lancamento.IdLancamento == 0 ? Encrypt.DecryptId(uri[3]) : lancamento.IdLancamento;
            lancamento = await _business.ObterLancamentoPorId(Id);
            return View(lancamento);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LancamentoDto lancamento)
        {
            string id = Encrypt.EncrypId(lancamento.IdLancamento);
            if (ModelState.IsValid)           
            {                
                lancamento.UsuarioId = Convert.ToInt32(User.Claims.Where(a => a.Type.Equals("IdUsuario")).First().Value);
                try
                {
                    if(await _business.Update(lancamento))
                        HttpContext.Session.SetString("success", "Lancamento atualizado com sucesso! ");
                    else
                        HttpContext.Session.SetString("error", "Ocorreu um erro ao atualizar o lancamento! ");                    
                    return RedirectToAction("Editar", lancamento);
                }
                catch(Exception ex)
                {
                    HttpContext.Session.SetString("error", ex.Message);
                    return RedirectToAction("Editar", lancamento);
                    //return RedirectToAction("Editar");
                }
            }
            else
            {
                HttpContext.Session.SetString("error", "Verifique os dados preenchidos! ");
            }
            return RedirectToAction("Editar", lancamento);
        }

        [HttpPost]
        public async Task<IActionResult> Cancelar(int IdLancamento)
        {
            int IdUsuario = Convert.ToInt32(User.Claims.Where(a => a.Type.Equals("IdUsuario")).First().Value);
            if (IdLancamento == 0)
            {
                HttpContext.Session.SetString("error", "Erro ao localizar o lancamento! ");
                return RedirectToAction("Index");
            }
            try
            {
                if (await _business.Cancelar(IdLancamento, IdUsuario))
                    HttpContext.Session.SetString("success", "Lancamento cancelado com sucesso! ");
                else
                    HttpContext.Session.SetString("error", "Ocorreu um erro ao cancelar o lancamento! ");
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                HttpContext.Session.SetString("error", ex.Message);
                return RedirectToAction("Index");
            }            
        }
    }
}