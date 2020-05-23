using AutoMapper;
using Loja.Business.Interfaces;
using Loja.Repository.Interfaces;
using Loja.Repository.Dtos;
using Loja.Business.Enumerador;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loja.Domain.Db;
using System.Linq;

namespace Loja.Business.Implementations
{
    public class PedidoBusiness : IPedidoBusiness
    {
        private readonly IPedidoRepository _repository;
        private readonly ILancamentoRepository _lancamento;
        private readonly IMapper _mapper;
        public PedidoBusiness(IMapper mapper, IPedidoRepository repository, ILancamentoRepository lancamento)
        {
            _repository = repository;
            _lancamento = lancamento;
            _mapper = mapper;
        }

        public async Task<bool> Create(PedidoDto pPedido)
        {
            bool result = false;
            var pedido = _mapper.Map<Pedido>(pPedido);
            try
            {                                
                Lancamento lancamento = new Lancamento
                {
                    DscLancamento = pedido.DescricaoPedido,                    
                    ValorLancamento = pedido.ValorTotal,
                    Baixado = (int)Enuns.Baixado.Nao,
                    FornecedorId=pedido.FornecedorId,
                    TipoPagamentoId=(int)Enuns.TipoPagamento.Boleto,
                    TipoLancamentoId=(int)Enuns.TipoLancamento.Pagamento,
                    UsuarioId=pedido.UsuarioId
                };

                if(await _lancamento.Add(lancamento))
                {
                    var lstId = await _lancamento.FindIdLancamento(lancamento.UsuarioId, lancamento.TipoLancamentoId);
                    pedido.LancamentoId = lstId.FirstOrDefault().IdLancamento;
                    if (await _repository.Add(pedido))
                        result = true;
                }                
            }
            catch (Exception ex)
            {
                _lancamento.DesfazerLancamento(pedido.LancamentoId);
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<bool> Delete(int pIdPedido)
        {
            bool result = false;
            try
            {
                if (await _repository.Delete(pIdPedido))
                    result = true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<PedidoDto> ObterPedidoPorId(int pIdPedido)
        {
            PedidoDto pedido;
            try
            {
                pedido = await _repository.FindById(pIdPedido);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return pedido;
        }

        public async Task<List<PedidoDto>> ObterTodosPedidos()
        {
            List<PedidoDto> lstPedidos;
            try
            {
                lstPedidos = await _repository.FindAllPedidos();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lstPedidos;
        }

        public async Task<bool> Update(PedidoDto pPedido)
        {
            bool result = false;
            try
            {
                pPedido.ValorTotal = pPedido.ValorTotal.Replace(",", ".");
                if (await _repository.Update(pPedido))
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
