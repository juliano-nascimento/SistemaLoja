using Loja.Repository.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Business.Interfaces
{
   public interface IUsuarioBusiness
    {
        Task<UsuarioDto> ValidarLogin(string Email, string Senha);
    }
}
