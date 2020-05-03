using System;
using System.Collections.Generic;

namespace Loja.Domain.Db
{
    public partial class StatusUsuario
    {
        public StatusUsuario()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int IdStatusUsuario { get; set; }
        public string DscStatusUsuario { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
