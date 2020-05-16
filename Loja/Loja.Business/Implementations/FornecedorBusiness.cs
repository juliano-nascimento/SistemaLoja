using AutoMapper;
using Loja.Business.Interfaces;
using Loja.Business.Enumerador;
using Loja.Repository.Dtos;
using Loja.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Loja.Domain.Db;

namespace Loja.Business.Implementations
{
   public class FornecedorBusiness : IFornecedorBusiness
    {
        private readonly IFornecedorRepository _repository;
        private readonly IMapper _mapper;
        public FornecedorBusiness(IMapper mapper, IFornecedorRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<bool> Create(FornecedorDto pFornecedor)
        {
            bool result = false;
            try
            {
                var fornecedor = _mapper.Map<Fornecedor>(pFornecedor);
                if (await _repository.Add(fornecedor))
                    result = true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<bool> Delete(int pIdFornecedor)
        {
            bool result = false;
            try
            {
                if (await _repository.Delete(pIdFornecedor))
                    result = true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<List<FornecedorDto>> ObterFornecedoresAtivos()
        {
            List<FornecedorDto> lstFornecedor;
            int StatusFornecedor = (int)Enuns.StatusFornecedor.Ativo;
            try
            {
                lstFornecedor = await _repository.FindByAtivos(StatusFornecedor);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lstFornecedor;
        }

        public async Task<FornecedorDto> ObterFornecedorPorId(int pId)
        {
            FornecedorDto fornecedor;
            try
            {
                fornecedor = await _repository.FindById(pId);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return fornecedor;
        }

        public async Task<bool> Update(FornecedorDto pFornecedor)
        {
            bool result = false;
            try
            {
                var fornecedor = _mapper.Map<Fornecedor>(pFornecedor);
                if (await _repository.Update(fornecedor))
                    result = true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
