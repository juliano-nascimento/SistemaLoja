using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using AutoMapper;
using Loja.Domain.Db;
using Loja.Repository.Dtos;
using Loja.Repository.Services;

namespace Loja.Business.Entitys
{
    public class ProdutoBusiness
    {
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutoBusiness(IMapper mapper, IProdutoService produtoService)
        {
            _mapper = mapper;
            _produtoService = produtoService;
        }

        public ProdutoBusiness()
        {

        }

        public async Task<bool> Add(ProdutoDto pProduto)
        {
            bool retorno = false;
            try
            {
                var produto = _mapper.Map<Produto>(pProduto);
                _produtoService.Add(produto);
                if (await _produtoService.SaveChangesAsync())
                    retorno = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retorno;
        }
    }
}
