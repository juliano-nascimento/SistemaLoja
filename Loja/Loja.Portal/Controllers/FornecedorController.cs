using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loja.Business.Interfaces;
using Loja.Repository.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loja.Portal.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly IFornecedorBusiness _business;
        public FornecedorController(IFornecedorBusiness business)
        {
            _business = business;
        }
        public async Task<IActionResult> Index()
        {
            FornecedorDto model = new FornecedorDto();
            model.ListaFornecedores = await _business.ObterFornecedoresAtivos();
            return View(model);
        }

        public IActionResult Adicionar(FornecedorDto Fornecedor)
        {
            return View(Fornecedor);
        }

        [HttpPost]
        public async Task<IActionResult> Adiciona(FornecedorDto fornecedor)
        {
            if (ModelState.IsValid)
            {
                if(await _business.Create(fornecedor))
                {
                    HttpContext.Session.SetString("success", "Fornecedor cadastrado com sucesso! ");
                    return RedirectToAction("Adicionar");
                }
                else
                    HttpContext.Session.SetString("error", "Ocorreu um erro ao cadastrar o fornecedor! ");
            }
            else
            {
                HttpContext.Session.SetString("error", "Verifique os dados preenchidos! ");
            }
            return RedirectToAction("Adicionar", fornecedor);
        }

        public async Task<IActionResult> Visualizar()
        {
            FornecedorDto model;
            string[] uri = HttpContext.Request.Path.ToUriComponent().Split("/");
            int Id = Convert.ToInt32(uri[3]);
            model = await _business.ObterFornecedorPorId(Id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int IdFornecedor)
        {
            if (IdFornecedor == 0)
            {
                HttpContext.Session.SetString("error", "Erro ao localizar o fornecedor! ");
                return RedirectToAction("Index");
            }
            if(await _business.Delete(IdFornecedor))
                HttpContext.Session.SetString("success", "Fornecedor deletado com sucesso! ");
            else
                HttpContext.Session.SetString("error", "Ocorreu um erro ao deletar o fornecedor! ");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(FornecedorDto fornecedor)
        {
            string[] uri = HttpContext.Request.Path.ToUriComponent().Split("/");
            int Id = fornecedor.IdFornecedor == 0 ? Convert.ToInt32(uri[3]) : fornecedor.IdFornecedor;
            fornecedor = await _business.ObterFornecedorPorId(Id);
            return View(fornecedor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FornecedorDto fornecedor)
        {
            if (ModelState.IsValid)
            {
                if(await _business.Update(fornecedor))
                    HttpContext.Session.SetString("success", "Fornecedor atualizado com sucesso! ");
                else
                    HttpContext.Session.SetString("error", "Ocorreu um erro ao atualizar o fornecedor! ");
            }
            else
                HttpContext.Session.SetString("error", "Verifique os dados preenchidos! ");

            return RedirectToAction("Editar", fornecedor);
        }
    }
}