using AutoMapper;
using Loja.Business.Interfaces;
using Loja.Domain.Db;
using Loja.Business.Enumerador;
using Loja.Repository.Dtos;
using Loja.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;

namespace Loja.Business.Implementations
{
    public class LancamentoBusiness : ILancamentoBusiness
    {
        private readonly ILancamentoRepository _repository;
        private readonly IContaRepository _conta;
        private readonly IMapper _mapper;
        public LancamentoBusiness(ILancamentoRepository repository, IMapper mapper, IContaRepository conta)
        {
            _repository = repository;
            _conta = conta;
            _mapper = mapper;
        }

        public async Task<bool> Create(LancamentoDto pLancamento)
        {
            bool result = false;
            pLancamento.Baixado = (int)Enuns.Baixado.Nao;            
            var lancamento = _mapper.Map<Lancamento>(pLancamento);
            try
            {
                if (await _repository.Add(lancamento))
                    result = true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<bool> Cancelar(int pId, int pIdUsuario)
        {
            bool result = false;
            try
            {
                if (await _repository.Cancelar(pId, pIdUsuario))
                    result = true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<LancamentoDto> ObterLancamentoPorId(int pId)
        {
            LancamentoDto lancamento;
            try
            {
                lancamento = await _repository.FindById(pId);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lancamento;
        }

        public async Task<List<LancamentoDto>> ObterLancamentos()
        {
            List<LancamentoDto> lancamentos;
            try
            {
                lancamentos = await _repository.FindAll();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lancamentos;
        }

        public async Task<bool> Update(LancamentoDto pLancamento)
        {
            bool result = false;
            try
            {
                pLancamento.ValorLancamento = pLancamento.ValorLancamento.Replace(",", ".");
                if (pLancamento.DataPagamento != null)
                    pLancamento.Baixado = (int)Enuns.Baixado.Sim;
                if (await _repository.Update(pLancamento))
                    result = true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<List<ContasDto>> ObterContas()
        {
            List<ContasDto> lstContas;
            try
            {
                lstContas = await _conta.FindAll();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lstContas;
        }
    }
}
