using System;
using System.Collections.Generic;

namespace Loja.Domain.Db
{
    public partial class AberturaCaixa
    {
        public AberturaCaixa()
        {
            Caixa = new HashSet<Caixa>();
        }

        public int IdAberturaCaixa { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime DataMovimento { get; set; }
        public int NumCaixa { get; set; }
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Caixa> Caixa { get; set; }
    }
}
