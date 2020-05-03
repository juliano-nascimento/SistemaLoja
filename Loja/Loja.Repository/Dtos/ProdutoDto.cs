using System;
using System.Collections.Generic;
using System.Text;

namespace Loja.Repository.Dtos
{
    public class ProdutoDto
    {
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public string Unidade { get; set; }
        public decimal PrecoCompra { get; set; }
        public decimal PrecoVenda { get; set; }
        public int Estoque { get; set; }
        public int? EstoqueMinimo { get; set; }
        public ulong? Saida { get; set; }
        public ulong? Entrada { get; set; }
        public string CodDeBarra { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public DateTime? DataValidade { get; set; }
        public int UsuarioId { get; set; }
    }
}
