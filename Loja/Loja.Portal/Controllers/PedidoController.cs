using Loja.Business.Interfaces;
using System.Threading.Tasks;
using Loja.Repository.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using Jarvis.Utils;

namespace Loja.Portal.Controllers
{
    [Authorize]
    public class PedidoController : Controller
    {
        private readonly IPedidoBusiness _business;
        public PedidoController(IPedidoBusiness business)
        {
            _business = business;
        }
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            PedidoDto pedido = new PedidoDto
            {
                ListaPedidos = await _business.ObterTodosPedidos()
            };
            return View(pedido);
        }
                
        public IActionResult Adicionar(PedidoDto pedido)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> Adiciona(PedidoDto pedido)
        {
            if (ModelState.IsValid)
            {
                pedido.UsuarioId = Convert.ToInt32(User.Claims.Where(a => a.Type.Equals("IdUsuario")).First().Value);                
                if (await _business.Create(pedido))
                {
                    HttpContext.Session.SetString("success", "Pedido cadastrado com sucesso! ");
                    return RedirectToAction("Adicionar");
                }
                else
                {
                    HttpContext.Session.SetString("error", "Ocorreu um erro ao cadastrar o pedido. ");
                }
            }
            else
            {
                HttpContext.Session.SetString("error", "Verifique os dados digitados! ");
            }
            return RedirectToAction("Adicionar", pedido);
        }

        public async Task<IActionResult> Visualizar()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            PedidoDto model;
            string[] uri = HttpContext.Request.Path.ToUriComponent().Split("/");
            int Id = Encrypt.DecryptId(uri[3]);
            model = await _business.ObterPedidoPorId(Id);
            return View(model);
        }

        public async Task<IActionResult> Editar(PedidoDto pedido)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            string[] uri = HttpContext.Request.Path.ToUriComponent().Split("/");
            int Id = pedido.IdPedido == 0 ? Encrypt.DecryptId(uri[3]) : pedido.IdPedido;
            pedido = await _business.ObterPedidoPorId(Id);
            return View(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PedidoDto pedido)
        {
            if (ModelState.IsValid)
            {
                if(await _business.Update(pedido))
                    HttpContext.Session.SetString("success", "Pedido atualizado com sucesso! ");
                else
                    HttpContext.Session.SetString("error", "Ocorreu um erro ao atualizar o pedido! ");
            }
            else
                HttpContext.Session.SetString("error", "Verifique os dados preenchidos! ");

            return RedirectToAction("Editar", pedido);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int IdPedido)
        {
            if (IdPedido == 0)
            {
                HttpContext.Session.SetString("error", "Erro ao localizar o pedido! ");
                return RedirectToAction("Index");
            }

            if(await _business.Delete(IdPedido))
                HttpContext.Session.SetString("success", "Pedido excluido com sucesso! ");
            else
                HttpContext.Session.SetString("error", "Ocorreu um erro ao deletar o pedido! ");

            return RedirectToAction("Index");
        }
    }
}