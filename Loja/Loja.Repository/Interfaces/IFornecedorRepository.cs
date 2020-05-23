using Loja.Domain.Db;
using Loja.Repository.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Repository.Interfaces
{
    public interface IFornecedorRepository
    {
        Task<List<FornecedorDto>> FindByAtivos(int pStatus);
        Task<bool> Add(Fornecedor fornecedor);
        Task<FornecedorDto> FindById(int pId);
        Task<bool> Delete(int pId);
        Task<bool> Update(Fornecedor pFornecedor);
        Task<List<string>> FindNomeIdPorTermo(string pTermo);
    }
}
