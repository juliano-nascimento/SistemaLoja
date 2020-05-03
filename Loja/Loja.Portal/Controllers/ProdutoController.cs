using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Loja.Business.Entitys;
using Loja.Repository.Dtos;
using Loja.Repository.Services;
using Microsoft.AspNetCore.Mvc;

namespace Loja.Portal.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;
        private ProdutoBusiness produtoBusiness;

        public ProdutoController(IProdutoService produtoService, IMapper mapper)
        {
            _mapper = mapper;
            _produtoService = produtoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Adicionar(ProdutoDto produto)
        {
            return View(produto);
        }

        public async Task<IActionResult> Adiciona(ProdutoDto produto)
        {
            produtoBusiness = new ProdutoBusiness(_mapper,_produtoService);
            if(produto == null)
            {
                ViewBag.Erro = "Preencha os dados do poduto! ";
                return RedirectToAction("Adicionar", produto);
            }       

            if (await produtoBusiness.Add(produto))
                return RedirectToAction("Adicionar", produto);
            else
            {
                ViewBag.Erro = "Erro ao adicionar o produto! ";
                return RedirectToAction("Adicionar", produto);
            }                
        }


    }
}