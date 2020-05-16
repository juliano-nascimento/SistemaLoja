using Loja.Domain.Db;
using Loja.Repository.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
