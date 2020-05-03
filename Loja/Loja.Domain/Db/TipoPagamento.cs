using System;
using System.Collections.Generic;

namespace Loja.Domain.Db
{
    public partial class TipoPagamento
    {
        public TipoPagamento()
        {
            Lancamento = new HashSet<Lancamento>();
            Venda = new HashSet<Venda>();
        }

        public int IdTipoPagamento { get; set; }
        public string DscTipoPagamento { get; set; }

        public virtual ICollection<Lancamento> Lancamento { get; set; }
        public virtual ICollection<Venda> Venda { get; set; }
    }
}
