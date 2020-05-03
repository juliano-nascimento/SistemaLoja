using System;
using System.Collections.Generic;

namespace Loja.Domain.Db
{
    public partial class TipoLancamento
    {
        public TipoLancamento()
        {
            Lancamento = new HashSet<Lancamento>();
        }

        public int IdTipoLancamento { get; set; }
        public string DscTipoLancamento { get; set; }

        public virtual ICollection<Lancamento> Lancamento { get; set; }
    }
}
