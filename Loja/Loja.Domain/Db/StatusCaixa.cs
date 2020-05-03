using System;
using System.Collections.Generic;

namespace Loja.Domain.Db
{
    public partial class StatusCaixa
    {
        public StatusCaixa()
        {
            Caixa = new HashSet<Caixa>();
        }

        public int IdStatusCaixa { get; set; }
        public string DscStatusCaixa { get; set; }

        public virtual ICollection<Caixa> Caixa { get; set; }
    }
}
