using System;
using System.Collections.Generic;

namespace Loja.Domain.Db
{
    public partial class Caixa
    {
        public Caixa()
        {
            Venda = new HashSet<Venda>();
            VendasItem = new HashSet<VendasItem>();
        }

        public int IdCaixa { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime DataMovimento { get; set; }
        public int NumCaixa { get; set; }
        public int StatusCaixaId { get; set; }
        public DateTime DataFechamento { get; set; }
        public decimal TotalVendas { get; set; }
        public decimal TotalDinheiro { get; set; }
        public decimal TotalCredito { get; set; }
        public decimal TotalDebito { get; set; }
        public int OperadorId { get; set; }
        public int AberturaId { get; set; }
        public int FechamentoId { get; set; }

        public virtual AberturaCaixa Abertura { get; set; }
        public virtual FechamentoCaixa Fechamento { get; set; }
        public virtual Usuario Operador { get; set; }
        public virtual StatusCaixa StatusCaixa { get; set; }
        public virtual ICollection<Venda> Venda { get; set; }
        public virtual ICollection<VendasItem> VendasItem { get; set; }
    }
}
