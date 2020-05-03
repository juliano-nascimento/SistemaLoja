using System;
using System.Collections.Generic;

namespace Loja.Domain.Db
{
    public partial class StatusVenda
    {
        public StatusVenda()
        {
            Venda = new HashSet<Venda>();
            VendasItem = new HashSet<VendasItem>();
        }

        public int IdStatusVenda { get; set; }
        public string DscStatusVenda { get; set; }

        public virtual ICollection<Venda> Venda { get; set; }
        public virtual ICollection<VendasItem> VendasItem { get; set; }
    }
}
