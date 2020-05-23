using Loja.Domain.Db;
using Loja.Repository.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Repository.Interfaces
{
    public interface IPedidoRepository
    {
        Task<List<PedidoDto>> FindAllPedidos();
        Task<bool> Add(Pedido pPedido);
        Task<PedidoDto> FindById(int pIdPedido);
        Task<bool> Update(PedidoDto pPedido);
        Task<bool> Delete(int pIdPedido);
    }
}
