using System;
using System.Collections.Generic;

namespace Loja.Domain.Db
{
    public partial class Venda
    {
        public Venda()
        {
            VendasItem = new HashSet<VendasItem>();
        }

        public int IdVenda { get; set; }
        public DateTime DataVenda { get; set; }
        public DateTime DataMovimento { get; set; }
        public int NumCaixa { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorLiquido { get; set; }
        public int UsuarioId { get; set; }
        public int CaixaId { get; set; }
        public int TipoPagamentoId { get; set; }
        public int StatusVendaId { get; set; }
        public DateTime? DataCancelamento { get; set; }
        public decimal TotalDebito { get; set; }
        public decimal TotalCredito { get; set; }
        public decimal TotalDinheiro { get; set; }

        public virtual Caixa Caixa { get; set; }
        public virtual StatusVenda StatusVenda { get; set; }
        public virtual TipoPagamento TipoPagamento { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<VendasItem> VendasItem { get; set; }
    }
}
