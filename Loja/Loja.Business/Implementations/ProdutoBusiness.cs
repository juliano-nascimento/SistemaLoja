using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Loja.Business.Interfaces;
using Loja.Domain.Db;
using Loja.Repository.Dtos;
using Loja.Repository.Interfaces;
using Loja.Repository.Models;

namespace Loja.Business.Implementations
{
    public class ProdutoBusiness : IProdutoBusiness
    {
        private readonly IProdutoRepository _repository;
        private readonly IMapper _mapper;

        public ProdutoBusiness(IMapper mapper, IProdutoRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public ProdutoBusiness()
        {

        }

        public async Task<bool> Create(ProdutoDto pProduto)
        {
            bool retorno = false;
            try
            {
                var produto = _mapper.Map<Produto>(pProduto);
                produto.UsuarioId = 1; //depois alterar para o usuario logado               
                if (produto.DataValidade != null)
                    produto.DataValidade = DateTime.Parse(produto.DataValidade).ToString("yyyy/MM/dd");
                if (await _repository.Add(produto))
                    retorno = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retorno;
        }

        public async Task<List<ProdutoModel>> ObterProdutos()
        {
            List<ProdutoModel> lstProdutos;
            try
            {
                lstProdutos = await _repository.FindAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return lstProdutos;
        }

        public async Task<ProdutoModel> ObterProdutoPorId(int pId)
        {
            ProdutoModel produto;
            try
            {
                produto = await _repository.FindById(pId);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return produto;
        }

        public async Task<bool> Update(ProdutoModel pProduto)
        {
            bool retorno = false;
            try
            {
                if(pProduto.DataValidadeEd != null)
                {
                    pProduto.DataValidadeEd = DateTime.Parse(pProduto.DataValidadeEd).ToString("yyyy/MM/dd");                                    
                }
                pProduto.UsuarioId = 1;
                pProduto.PrecoCompra = pProduto.PrecoCompra.Replace(",", ".");
                pProduto.PrecoVenda = pProduto.PrecoVenda.Replace(",", ".");

                if (await _repository.Update(pProduto))
                    retorno = true;                    
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retorno;
        }

        public async Task<bool> Delete(int pId)
        {
            bool retorno = false;
            try
            {
                if (await _repository.Delete(pId))
                    retorno = true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retorno;
        }

        public async Task<bool> AtualizarEstoque(int pId, int pEstoque)
        {
            bool retorno = false;
            try
            {
                if (await _repository.UpdateEstoque(pId, pEstoque))
                    retorno = true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retorno;
        }
    }
}
