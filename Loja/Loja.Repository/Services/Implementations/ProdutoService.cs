using Loja.Domain.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Repository.Services.Implementations
{
   public class ProdutoService : IProdutoService
    {
        private readonly dblojaContext _context;

        public ProdutoService(dblojaContext context)
        {
            _context = context;
        }
        public void Add(Produto produto)
        {
            try
            {
                _context.Produto.Add(produto);               
            }
            catch(Exception ex)
            {
                throw new Exception("Erro ao adicionar produto. " + ex.Message);
            }            
        }

        public async Task<List<Produto>> FindAll()
        {
            try
            {
                return  _context.Produto.ToList();
            }
            catch(Exception ex)
            {
                throw new Exception("Erro ao consultar todos os produtos. " + ex.Message);
            }
        }

        public Task<Produto> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
