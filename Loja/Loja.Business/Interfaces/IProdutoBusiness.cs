using Loja.Repository.Dtos;
using Loja.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Business.Interfaces
{
    public interface IProdutoBusiness
    {
        Task<bool> Create(ProdutoDto pProduto);
        Task<List<ProdutoModel>> ObterProdutos();
        Task<ProdutoModel> ObterProdutoPorId(int pId);
        Task<bool> Update(ProdutoModel pProduto);
        Task<bool> Delete(int pId);
        Task<bool> AtualizarEstoque(int pId, int pEstoque);
    }
}
