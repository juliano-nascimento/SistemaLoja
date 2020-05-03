using System;
using System.Collections.Generic;

namespace Loja.Domain.Db
{
    public partial class NivelUsuario
    {
        public NivelUsuario()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int IdNivelUsuario { get; set; }
        public string DscNivelUsuario { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
