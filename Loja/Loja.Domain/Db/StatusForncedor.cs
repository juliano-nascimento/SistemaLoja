using System;
using System.Collections.Generic;

namespace Loja.Domain.Db
{
    public partial class StatusForncedor
    {
        public StatusForncedor()
        {
            Fornecedor = new HashSet<Fornecedor>();
        }

        public int IdStatusForncedor { get; set; }
        public string DscStatusForncedor { get; set; }

        public virtual ICollection<Fornecedor> Fornecedor { get; set; }
    }
}
