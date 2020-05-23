using System.Threading.Tasks;
using Loja.Business.Interfaces;
using Loja.Repository.Dtos;
using Loja.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Jarvis.Utils;

namespace Loja.Portal.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {
        private readonly IProdutoBusiness _business;

        public ProdutoController(IProdutoBusiness business)
        {
            _business = business;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            ProdutoModel model = new ProdutoModel();
            model.ListaProdutos = await _business.ObterProdutos();
            return View(model);
        }

        public IActionResult Adicionar(ProdutoDto produto)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Adiciona(ProdutoDto produto)
        {
            if (ModelState.IsValid)
            {
                if (await _business.Create(produto))
                {
                    HttpContext.Session.SetString("success", "Produto cadastrado com sucesso! ");
                    return RedirectToAction("Adicionar");
                }
                else
                    HttpContext.Session.SetString("error", "Ocorreu um erro ao cadastrar o produto! ");
            }
            else
            {
                HttpContext.Session.SetString("error", "Preencha todos os dados! ");
            }
            return RedirectToAction("Adicionar", produto);
        }

        public async Task<IActionResult> Visualizar()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            ProdutoModel model;
            string[] uri = HttpContext.Request.Path.ToUriComponent().Split("/");
            int Id = Encrypt.DecryptId(uri[3]);
            model = await _business.ObterProdutoPorId(Id);
            return View(model);
        }

        public async Task<IActionResult> Editar(ProdutoModel produto)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            string[] uri = HttpContext.Request.Path.ToUriComponent().Split("/");
            int Id = produto.IdProduto == 0 ? Encrypt.DecryptId(uri[3]) : produto.IdProduto;
            produto = await _business.ObterProdutoPorId(Id);
            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProdutoModel pModel)
        {
            if (ModelState.IsValid)
            {
                if (await _business.Update(pModel))
                {
                    HttpContext.Session.SetString("success", "Produto atualizado com sucesso! ");
                    return RedirectToAction("Editar", pModel);
                }
                else
                {
                    HttpContext.Session.SetString("error", "Ocorreu um erro ao atualizar o produto! ");
                }
            }
            else
            {
                HttpContext.Session.SetString("error", "Preencha todos os dados! ");
            }
            return RedirectToAction("Editar", pModel);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int IdProduto)
        {
            if (IdProduto == 0)
            {
                HttpContext.Session.SetString("error", "Erro ao localizar o produto! ");
                return RedirectToAction("Index");
            }

            if (await _business.Delete(IdProduto))
            {
                HttpContext.Session.SetString("success", "Produto deletado com sucesso! ");
                return RedirectToAction("Index");
            }
            else
            {
                HttpContext.Session.SetString("error", "Ocorreu um erro ao deletar o produto! ");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarEstoque(ProdutoModel pProduto)
        {
            if (pProduto.IdProduto == 0)
            {
                HttpContext.Session.SetString("error", "Erro ao localizar o produto! ");
                return RedirectToAction("Index");
            }
            pProduto.Estoque += pProduto.EstoqueAtual;
            if (await _business.AtualizarEstoque(pProduto.IdProduto, pProduto.Estoque))
                HttpContext.Session.SetString("success", "Estoque Atualizado com sucesso! ");
            else
                HttpContext.Session.SetString("error", "Erro ao atualizar o estoque! ");

            return RedirectToAction("Index");
        }
    }
}