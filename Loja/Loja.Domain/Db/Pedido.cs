using System;
using System.Collections.Generic;

namespace Loja.Domain.Db
{
    public partial class Pedido
    {
        public int IdPedido { get; set; }
        public string DescricaoPedido { get; set; }
        public DateTime DataPedido { get; set; }
        public string ValorTotal { get; set; }
        public string DscProdutos { get; set; }
        public int UsuarioId { get; set; }
        public int StatusPedidoId { get; set; }
        public int LancamentoId { get; set; }
        public int FornecedorId { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }
        public virtual Lancamento Lancamento { get; set; }
        public virtual StatusPedido StatusPedido { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
