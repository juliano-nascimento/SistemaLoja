using System.Threading.Tasks;
using Loja.Business.Interfaces;
using Loja.Repository.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Jarvis.Utils;
using System.Text.Encodings.Web;
using System.Linq;
using System.Collections.Generic;

namespace Loja.Portal.Controllers
{
    [Authorize]
    public class FornecedorController : Controller
    {
        private readonly IFornecedorBusiness _business;

        public FornecedorController(IFornecedorBusiness business, UrlEncoder encoder)
        {
            _business = business;
        }
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            FornecedorDto model = new FornecedorDto();
            model.ListaFornecedores = await _business.ObterFornecedoresAtivos();
            return View(model);
        }

        public IActionResult Adicionar(FornecedorDto Fornecedor)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(Fornecedor);
        }

        [HttpPost]
        public async Task<IActionResult> Adiciona(FornecedorDto fornecedor)
        {
            if (ModelState.IsValid)
            {
                if (await _business.Create(fornecedor))
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
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            FornecedorDto model;
            string[] uri = HttpContext.Request.Path.ToUriComponent().Split("/");
            int Id = Encrypt.DecryptId(uri[3]);
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
            if (await _business.Delete(IdFornecedor))
                HttpContext.Session.SetString("success", "Fornecedor deletado com sucesso! ");
            else
                HttpContext.Session.SetString("error", "Ocorreu um erro ao deletar o fornecedor! ");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(FornecedorDto fornecedor)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }

            string[] uri = HttpContext.Request.Path.ToUriComponent().Split("/");
            int Id = fornecedor.IdFornecedor == 0 ? Encrypt.DecryptId(uri[3]) : fornecedor.IdFornecedor;
            fornecedor = await _business.ObterFornecedorPorId(Id);
            return View(fornecedor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FornecedorDto fornecedor)
        {
            if (ModelState.IsValid)
            {
                if (await _business.Update(fornecedor))
                    HttpContext.Session.SetString("success", "Fornecedor atualizado com sucesso! ");
                else
                    HttpContext.Session.SetString("error", "Ocorreu um erro ao atualizar o fornecedor! ");
            }
            else
                HttpContext.Session.SetString("error", "Verifique os dados preenchidos! ");

            return RedirectToAction("Editar", fornecedor);
        }

        [HttpPost]
        public async Task<string[]> GetClientes()
        {
            string termo = Request.Form.Keys.First();
            List<string> fornecedores;
            fornecedores = await _business.ObterNomeIdFornecedores(termo);

            return fornecedores.ToArray();
        }
    }
}