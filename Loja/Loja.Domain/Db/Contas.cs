using System;
using System.Collections.Generic;

namespace Loja.Domain.Db
{
    public partial class Contas
    {
        public Contas()
        {
            Lancamento = new HashSet<Lancamento>();
        }

        public int IdConta { get; set; }
        public string Banco { get; set; }
        public string Conta { get; set; }
        public string Agencia { get; set; }
        public decimal? Saldo { get; set; }

        public virtual ICollection<Lancamento> Lancamento { get; set; }
    }
}
