using System;
using System.Collections.Generic;

namespace Loja.Domain.Db
{
    public partial class StatusPedido
    {
        public StatusPedido()
        {
            Pedido = new HashSet<Pedido>();
        }

        public int IdStatusPedido { get; set; }
        public string DscStatusPedido { get; set; }

        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}
