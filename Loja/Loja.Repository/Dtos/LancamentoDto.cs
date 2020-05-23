using System;
using System.Collections.Generic;
using System.Text;

namespace Loja.Repository.Dtos
{
    public class LancamentoDto
    {
        public int IdLancamento { get; set; }
        public string DscLancamento { get; set; }
        public DateTime DataLancamento { get; set; }
        public decimal ValorLancamento { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public ulong Baixado { get; set; }
        public int FornecedorId { get; set; }
        public int TipoPagamentoId { get; set; }
        public int TipoLancamentoId { get; set; }
        public int? ContaId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime? DataCancelamento { get; set; }

        public List<LancamentoDto> ListaLancamento { get; set; }
    }
}
