using Loja.Repository.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Repository.Interfaces
{
   public interface IUsuarioRepository
    {
        Task<UsuarioDto> FindByEmailAsync(string Email);
    }
}
