using Loja.Repository.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Business.Interfaces
{
    public interface IPedidoBusiness
    {
        Task<List<PedidoDto>> ObterTodosPedidos();
        Task<bool> Create(PedidoDto pPedido);
        Task<PedidoDto> ObterPedidoPorId(int pIdPedido);
        Task<bool> Update(PedidoDto pPedido);
        Task<bool> Delete(int pIdPedido);
    }
}
