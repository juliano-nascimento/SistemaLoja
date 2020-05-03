﻿using System;
using System.Collections.Generic;

namespace Loja.Domain.Db
{
    public partial class Fornecedor
    {
        public Fornecedor()
        {
            Lancamento = new HashSet<Lancamento>();
        }

        public int IdFornecedor { get; set; }
        public string NomeFornecedor { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public int StatusFornecedorId { get; set; }

        public virtual StatusForncedor StatusFornecedor { get; set; }
        public virtual ICollection<Lancamento> Lancamento { get; set; }
    }
}
