using System;
using System.Collections.Generic;

namespace Loja.Domain.Db
{
    public partial class VendasItem
    {
        public int IdVendasItem { get; set; }
        public DateTime DtVendaItem { get; set; }
        public DateTime DataMovimento { get; set; }
        public int ProdutoId { get; set; }
        public decimal PrecoProduto { get; set; }
        public int QtdProduto { get; set; }
        public decimal PrecoTotal { get; set; }
        public int VendaId { get; set; }
        public int NumCaixa { get; set; }
        public int UsuarioId { get; set; }
        public int CaixaId { get; set; }
        public int StatusItemId { get; set; }
        public int StatusVendaId { get; set; }
        public DateTime? DataCancelamento { get; set; }

        public virtual Caixa Caixa { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual StatusItem StatusItem { get; set; }
        public virtual StatusVenda StatusVenda { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Venda Venda { get; set; }
    }
}
