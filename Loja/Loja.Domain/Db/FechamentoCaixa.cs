using System;
using System.Collections.Generic;

namespace Loja.Domain.Db
{
    public partial class FechamentoCaixa
    {
        public FechamentoCaixa()
        {
            Caixa = new HashSet<Caixa>();
        }

        public int IdFechamentoCaixa { get; set; }
        public DateTime DataFechamento { get; set; }
        public DateTime DataMovimento { get; set; }
        public int NumCaixa { get; set; }
        public int UsuarioId { get; set; }
        public int? LancamentoId { get; set; }

        public virtual Lancamento Lancamento { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Caixa> Caixa { get; set; }
    }
}
