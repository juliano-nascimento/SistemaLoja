using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Loja.Repository.Dtos
{
    public class PedidoDto
    {
        public PedidoDto()
        {

        }

        public PedidoDto(MySqlDataReader pReader)
        {
            var colunas = pReader.GetColumnSchema();
            if (colunas.Any(c => c.ColumnName.Equals("IdPedido")) && !string.IsNullOrEmpty(pReader["IdPedido"].ToString()))
                IdPedido = (int)pReader["IdPedido"];
            if (colunas.Any(c => c.ColumnName.Equals("DescricaoPedido")) && !string.IsNullOrEmpty(pReader["DescricaoPedido"].ToString()))
                DescricaoPedido = pReader["DescricaoPedido"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("DataPedido")) && !string.IsNullOrEmpty(pReader["DataPedido"].ToString()))
                DataPedido = (DateTime)pReader["DataPedido"];
            if (colunas.Any(c => c.ColumnName.Equals("ValorTotal")) && !string.IsNullOrEmpty(pReader["ValorTotal"].ToString()))
                ValorTotal = pReader["ValorTotal"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("DscProdutos")) && !string.IsNullOrEmpty(pReader["DscProdutos"].ToString()))
                DscProdutos = pReader["DscProdutos"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Usuario_Id")) && !string.IsNullOrEmpty(pReader["Usuario_Id"].ToString()))
                UsuarioId = (int)pReader["Usuario_Id"];
            if (colunas.Any(c => c.ColumnName.Equals("StatusPedido_Id")) && !string.IsNullOrEmpty(pReader["StatusPedido_Id"].ToString()))
                StatusPedidoId = (int)pReader["StatusPedido_Id"];
            if (colunas.Any(c => c.ColumnName.Equals("Lancamento_Id")) && !string.IsNullOrEmpty(pReader["Lancamento_Id"].ToString()))
                LancamentoId = (int)pReader["Lancamento_Id"];
            if (colunas.Any(c => c.ColumnName.Equals("NomeUsuario")) && !string.IsNullOrEmpty(pReader["NomeUsuario"].ToString()))
                NomeUsuario = pReader["NomeUsuario"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("DscStatusPedido")) && !string.IsNullOrEmpty(pReader["DscStatusPedido"].ToString()))
                DscStatusPedido = pReader["DscStatusPedido"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Fornecedor_Id")) && !string.IsNullOrEmpty(pReader["Fornecedor_Id"].ToString()))
                FornecedorId = (int)pReader["Fornecedor_Id"];
            if (colunas.Any(c => c.ColumnName.Equals("NomeFornecedor")) && !string.IsNullOrEmpty(pReader["NomeFornecedor"].ToString()))
                NomeFornecedor = pReader["NomeFornecedor"].ToString();
        }

        public int IdPedido { get; set; }
        public string DescricaoPedido { get; set; }

        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime DataPedido { get; set; }
        public string ValorTotal { get; set; }
        public string DscProdutos { get; set; }
        public int UsuarioId { get; set; }
        public int StatusPedidoId { get; set; }
        public int LancamentoId { get; set; }
        public string NomeUsuario { get; set; }
        public string DscStatusPedido { get; set; }
        public int FornecedorId { get; set; }
        public string NomeFornecedor { get; set; }

        public List<PedidoDto> ListaPedidos { get; set; }
    }
}
