using Loja.Repository.Dtos;
using System.Threading.Tasks;

namespace Loja.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<UsuarioDto> FindByEmailAsync(string Email);
    }
}
