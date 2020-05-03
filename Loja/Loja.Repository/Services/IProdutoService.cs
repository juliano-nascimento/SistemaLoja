using Loja.Domain.Db;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Repository.Services
{
   public interface IProdutoService
    {
        void Add(Produto produto);
        Task<Produto> FindById(int id);
        Task<List<Produto>> FindAll();
        Task<bool> SaveChangesAsync();
    }
}
