using System;
using System.Collections.Generic;

namespace Loja.Domain.Db
{
    public partial class StatusFornecedor
    {
        public StatusFornecedor()
        {
            Fornecedor = new HashSet<Fornecedor>();
        }

        public int IdStatusFornecedor { get; set; }
        public string DscStatusFornecedor { get; set; }

        public virtual ICollection<Fornecedor> Fornecedor { get; set; }
    }
}
