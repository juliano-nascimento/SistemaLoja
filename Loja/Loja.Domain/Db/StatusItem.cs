using System;
using System.Collections.Generic;

namespace Loja.Domain.Db
{
    public partial class StatusItem
    {
        public StatusItem()
        {
            VendasItem = new HashSet<VendasItem>();
        }

        public int IdStatusItem { get; set; }
        public string DscStatusItem { get; set; }

        public virtual ICollection<VendasItem> VendasItem { get; set; }
    }
}
