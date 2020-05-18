using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Loja.Business.Interfaces;
using Loja.Repository.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Loja.Portal.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioBusiness _business;
        public LoginController(IUsuarioBusiness business)
        {
            _business = business;
        }
        public IActionResult Index(UsuarioDto usuario)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View(usuario);
        }

        [HttpPost]
        public async Task<JsonResult> VerificarLogin(string Email, string Senha)
        {
            UsuarioDto usuario = new UsuarioDto();
            string json;
            try
            {
                if(Email != null)
                {
                    usuario = await _business.ValidarLogin(Email, Senha);
                    if (usuario.Retorno)
                    {
                        if(usuario.DataExpiracao < DateTime.Now)
                        {
                            usuario.Result = false;
                            usuario.Mensagem = "Acesso Expirado Entre em contato com o Administrador do Sistema!";
                            json = JsonConvert.SerializeObject(usuario);
                            return Json(json);
                            
                        }

                        Logar(usuario);
                        usuario.Result = true;
                        json = JsonConvert.SerializeObject(usuario);
                        return Json(json);
                    }
                    else
                    {
                        usuario.Result = false;
                        usuario.Mensagem = "Usuário e / ou Senha incorretos! ";
                        json = JsonConvert.SerializeObject(usuario);
                        return Json(json);
                    }
                }
                else
                {
                    usuario.Result = false;
                    usuario.Mensagem = "Insira o E-mail! ";
                    json = JsonConvert.SerializeObject(usuario);
                    return Json(json);
                }

            }
            catch(Exception)
            {
                usuario.Result = false;
                usuario.Mensagem = "Ocorreu algum erro na tentativa de login, Tente Novamente! ";
                json = JsonConvert.SerializeObject(usuario);
                return Json(json);
            }           
        }

        private async void Logar(UsuarioDto usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.NomeUsuario),
                new Claim("Documento", usuario.Cpf),
                new Claim("IdUsuario", usuario.IdUsuario.ToString()),
                new Claim("NivelUsuario", usuario.NivelUsuarioId.ToString())
            };

            var identidadeUsuario = new ClaimsIdentity(claims, "Login");

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identidadeUsuario);
            var propriedadeAutenticacao = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.Now.AddHours(1),
                IsPersistent = true
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, propriedadeAutenticacao);
            
        }
    }
}