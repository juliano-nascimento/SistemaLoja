using Loja.Domain.Db;
using Loja.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Repository.Interfaces
{
    public interface IProdutoRepository
    {
        Task<bool> Add(Produto produto);
        Task<ProdutoModel> FindById(int id);
        Task<List<ProdutoModel>> FindAll();
        Task<bool> Update(ProdutoModel pProduto);
        Task<bool> Delete(int pId);
        Task<bool> UpdateEstoque(int pId, int pEstoque);
    }
}
