using System;
using System.Collections.Generic;

namespace Loja.Domain.Db
{
    public partial class Usuario
    {
        public Usuario()
        {
            AberturaCaixa = new HashSet<AberturaCaixa>();
            Caixa = new HashSet<Caixa>();
            FechamentoCaixa = new HashSet<FechamentoCaixa>();
            Lancamento = new HashSet<Lancamento>();
            Pedido = new HashSet<Pedido>();
            Produto = new HashSet<Produto>();
            Venda = new HashSet<Venda>();
            VendasItem = new HashSet<VendasItem>();
        }

        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string Cpf { get; set; }
        public string EmailUsuario { get; set; }
        public string Rg { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public DateTime DataCadastro { get; set; }
        public int NivelUsuarioId { get; set; }
        public int StatusUsuarioId { get; set; }
        public DateTime DataExpiracao { get; set; }

        public virtual NivelUsuario NivelUsuario { get; set; }
        public virtual StatusUsuario StatusUsuario { get; set; }
        public virtual ICollection<AberturaCaixa> AberturaCaixa { get; set; }
        public virtual ICollection<Caixa> Caixa { get; set; }
        public virtual ICollection<FechamentoCaixa> FechamentoCaixa { get; set; }
        public virtual ICollection<Lancamento> Lancamento { get; set; }
        public virtual ICollection<Pedido> Pedido { get; set; }
        public virtual ICollection<Produto> Produto { get; set; }
        public virtual ICollection<Venda> Venda { get; set; }
        public virtual ICollection<VendasItem> VendasItem { get; set; }
    }
}
