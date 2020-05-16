using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Loja.Business.Interfaces;
using Loja.Repository.Dtos;
using Loja.Repository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loja.Portal.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoBusiness _business;

        public ProdutoController(IProdutoBusiness business)
        {
            _business = business;
        }

        public async Task<IActionResult> Index()
        {
            ProdutoModel model = new ProdutoModel();
            model.ListaProdutos = await _business.ObterProdutos();
            return View(model);
        }

        public IActionResult Adicionar(ProdutoDto produto)
        {
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
            ProdutoModel model;
            string[] uri = HttpContext.Request.Path.ToUriComponent().Split("/");
            int Id = Convert.ToInt32(uri[3]);
            model = await _business.ObterProdutoPorId(Id);
            return View(model);
        }

        public async Task<IActionResult> Editar(ProdutoModel produto)
        {
            string[] uri = HttpContext.Request.Path.ToUriComponent().Split("/");
            int Id = produto.IdProduto == 0 ? Convert.ToInt32(uri[3]) : produto.IdProduto;
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