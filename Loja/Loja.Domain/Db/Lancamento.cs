using System;
using System.Collections.Generic;

namespace Loja.Domain.Db
{
    public partial class Lancamento
    {
        public Lancamento()
        {
            FechamentoCaixa = new HashSet<FechamentoCaixa>();
            Pedido = new HashSet<Pedido>();
        }

        public int IdLancamento { get; set; }
        public string DscLancamento { get; set; }
        public DateTime DataLancamento { get; set; }
        public string ValorLancamento { get; set; }
        public DateTime? DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public ulong Baixado { get; set; }
        public int FornecedorId { get; set; }
        public int TipoPagamentoId { get; set; }
        public int TipoLancamentoId { get; set; }
        public int? ContaId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime? DataCancelamento { get; set; }

        public virtual Contas Conta { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual TipoLancamento TipoLancamento { get; set; }
        public virtual TipoPagamento TipoPagamento { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<FechamentoCaixa> FechamentoCaixa { get; set; }
        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}
