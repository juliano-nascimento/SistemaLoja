using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Loja.Repository.Models
{
   public class ProdutoModel
    {
        public ProdutoModel()
        {

        }

        public ProdutoModel(MySqlDataReader pReader)
        {
            //pega todas as colunas que retornaram no reader
            var colunas = pReader.GetColumnSchema();
            //verifica se a coluna existe no reader, para poder atribuir a propriedade da model
            if (colunas.Any(c => c.ColumnName.Equals("IdProduto")) && !string.IsNullOrEmpty(pReader["IdProduto"].ToString()))
                IdProduto = (int)pReader["IdProduto"];
            if (colunas.Any(c => c.ColumnName.Equals("NomeProduto")) && !string.IsNullOrEmpty(pReader["NomeProduto"].ToString()))
                NomeProduto = pReader["NomeProduto"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Unidade")) && !string.IsNullOrEmpty(pReader["Unidade"].ToString()))
                Unidade = pReader["Unidade"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("PrecoCompra")) && !string.IsNullOrEmpty(pReader["PrecoCompra"].ToString()))
                PrecoCompra = pReader["PrecoCompra"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("PrecoVenda")) && !string.IsNullOrEmpty(pReader["PrecoVenda"].ToString()))
                PrecoVenda = pReader["PrecoVenda"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Estoque")) && !string.IsNullOrEmpty(pReader["Estoque"].ToString()))
                Estoque = (int)pReader["Estoque"];
            if (colunas.Any(c => c.ColumnName.Equals("EstoqueMinimo")) && !string.IsNullOrEmpty(pReader["EstoqueMinimo"].ToString()))
                EstoqueMinimo = (int)pReader["EstoqueMinimo"];
            if (colunas.Any(c => c.ColumnName.Equals("Saida")) && !string.IsNullOrEmpty(pReader["Saida"].ToString()))
                Saida = (ulong)pReader["Saida"];
            if (colunas.Any(c => c.ColumnName.Equals("Entrada")) && !string.IsNullOrEmpty(pReader["Saida"].ToString()))
                Entrada = (ulong)pReader["Entrada"];
            if (colunas.Any(c => c.ColumnName.Equals("CodDeBarra")) && !string.IsNullOrEmpty(pReader["CodDeBarra"].ToString()))
                CodDeBarra = pReader["CodDeBarra"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("DataCadastro")) && !string.IsNullOrEmpty(pReader["DataCadastro"].ToString()))
                DataCadastro = (DateTime)pReader["DataCadastro"];
            if (colunas.Any(c => c.ColumnName.Equals("DataAtualizacao")) && !string.IsNullOrEmpty(pReader["DataAtualizacao"].ToString()))
                DataAtualizacao = (DateTime)pReader["DataAtualizacao"];
            if (colunas.Any(c => c.ColumnName.Equals("DataValidade")) && !string.IsNullOrEmpty(pReader["DataValidade"].ToString()))
                DataValidade = (DateTime)pReader["DataValidade"];
            if (colunas.Any(c => c.ColumnName.Equals("UsuarioId")) && !string.IsNullOrEmpty(pReader["UsuarioId"].ToString()))
                UsuarioId = (int)pReader["UsuarioId"];
            if (colunas.Any(c => c.ColumnName.Equals("RetEntrada")) && !string.IsNullOrEmpty(pReader["RetEntrada"].ToString()))
                RetEntrada = pReader["RetEntrada"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("RetSaida")) && !string.IsNullOrEmpty(pReader["RetSaida"].ToString()))
                RetSaida = pReader["RetSaida"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("NomeUsuario")) && !string.IsNullOrEmpty(pReader["NomeUsuario"].ToString()))
                NomeUsuario = pReader["NomeUsuario"].ToString();
        }
        
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public string Unidade { get; set; }
        public string PrecoCompra { get; set; }
        public string PrecoVenda { get; set; }
        public int Estoque { get; set; }
        public int? EstoqueMinimo { get; set; }
        public ulong? Saida { get; set; }
        public ulong? Entrada { get; set; }
        public string CodDeBarra { get; set; }

        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime DataCadastro { get; set; }

        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime DataAtualizacao { get; set; }

        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime? DataValidade { get; set; }
        public int UsuarioId { get; set; }
        public string RetEntrada { get; set; }
        public string RetSaida { get; set; }
        public string NomeUsuario { get; set; }
        public string DataValidadeEd { get; set; }
        public int EstoqueAtual { get; set; }

        public List<ProdutoModel> ListaProdutos { get; set; }
    }
}
