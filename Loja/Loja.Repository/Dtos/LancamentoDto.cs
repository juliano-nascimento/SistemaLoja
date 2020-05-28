using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Loja.Repository.Dtos
{
    public class LancamentoDto
    {
        public LancamentoDto(MySqlDataReader pReader)
        {
            var colunas = pReader.GetColumnSchema();
            if (colunas.Any(c => c.ColumnName.Equals("IdLancamento")) && !string.IsNullOrEmpty(pReader["IdLancamento"].ToString()))
                IdLancamento = (int)pReader["IdLancamento"];
            if (colunas.Any(c => c.ColumnName.Equals("DscLancamento")) && !string.IsNullOrEmpty(pReader["DscLancamento"].ToString()))
                DscLancamento = pReader["DscLancamento"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("DataLancamento")) && !string.IsNullOrEmpty(pReader["DataLancamento"].ToString()))
                DataLancamento = (DateTime)pReader["DataLancamento"];
            if (colunas.Any(c => c.ColumnName.Equals("ValorLancamento")) && !string.IsNullOrEmpty(pReader["ValorLancamento"].ToString()))
                ValorLancamento = pReader["ValorLancamento"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("DataVencimento")) && !string.IsNullOrEmpty(pReader["DataVencimento"].ToString()))
                DataVencimento = DateTime.Parse(pReader["DataVencimento"].ToString()).ToString("dd/MM/yyyy");
            if (colunas.Any(c => c.ColumnName.Equals("DataPagamento")) && !string.IsNullOrEmpty(pReader["DataPagamento"].ToString()))
                DataPagamento = DateTime.Parse(pReader["DataPagamento"].ToString()).ToString("dd/MM/yyyy");
            if (colunas.Any(c => c.ColumnName.Equals("Baixado")) && !string.IsNullOrEmpty(pReader["Baixado"].ToString()))
                Baixado = (ulong)pReader["Baixado"];
            if (colunas.Any(c => c.ColumnName.Equals("Fornecedor_Id")) && !string.IsNullOrEmpty(pReader["Fornecedor_Id"].ToString()))
                FornecedorId = (int)pReader["Fornecedor_Id"];
            if (colunas.Any(c => c.ColumnName.Equals("NomeFornecedor")) && !string.IsNullOrEmpty(pReader["NomeFornecedor"].ToString()))
                NomeFornecedor = pReader["NomeFornecedor"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("TipoPagamento_Id")) && !string.IsNullOrEmpty(pReader["TipoPagamento_Id"].ToString()))
                TipoPagamentoId = (int)pReader["TipoPagamento_Id"];
            if (colunas.Any(c => c.ColumnName.Equals("DscTipoPagamento")) && !string.IsNullOrEmpty(pReader["DscTipoPagamento"].ToString()))
                DscTipoPagamento = pReader["DscTipoPagamento"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("TipoLancamento_Id")) && !string.IsNullOrEmpty(pReader["TipoLancamento_Id"].ToString()))
                TipoLancamentoId = (int)pReader["TipoLancamento_Id"];
            if (colunas.Any(c => c.ColumnName.Equals("DscTipoLancamento")) && !string.IsNullOrEmpty(pReader["DscTipoLancamento"].ToString()))
                DscTipoLancamento = pReader["DscTipoLancamento"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Conta_Id")) && !string.IsNullOrEmpty(pReader["Conta_Id"].ToString()))
                ContaId = (int)pReader["Conta_Id"];
            if (colunas.Any(c => c.ColumnName.Equals("Usuario_Id")) && !string.IsNullOrEmpty(pReader["Usuario_Id"].ToString()))
                UsuarioId = (int)pReader["Usuario_Id"];
            if (colunas.Any(c => c.ColumnName.Equals("NomeUsuario")) && !string.IsNullOrEmpty(pReader["NomeUsuario"].ToString()))
                NomeUsuario = pReader["NomeUsuario"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("DataCancelamento")) && !string.IsNullOrEmpty(pReader["DataCancelamento"].ToString()))
                DataCancelamento = (DateTime)pReader["DataCancelamento"];
            if (colunas.Any(c => c.ColumnName.Equals("Banco")) && !string.IsNullOrEmpty(pReader["Banco"].ToString()))
                Banco = pReader["Banco"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Agencia")) && !string.IsNullOrEmpty(pReader["Agencia"].ToString()))
                Agencia = pReader["Agencia"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Conta")) && !string.IsNullOrEmpty(pReader["Conta"].ToString()))
                Conta = pReader["Conta"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Saldo")) && !string.IsNullOrEmpty(pReader["Saldo"].ToString()))
                Saldo = (decimal)pReader["Saldo"];
            if (colunas.Any(c => c.ColumnName.Equals("StatusLancamento")) && !string.IsNullOrEmpty(pReader["StatusLancamento"].ToString()))
                StatusLancamento = pReader["StatusLancamento"].ToString();
        }
        public LancamentoDto()
        {

        }
        public int IdLancamento { get; set; }
        public string DscLancamento { get; set; }

        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime DataLancamento { get; set; }
        public string ValorLancamento { get; set; }

        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public string DataVencimento { get; set; }

        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public string DataPagamento { get; set; }
        public ulong Baixado { get; set; }
        public int? FornecedorId { get; set; }
        public int TipoPagamentoId { get; set; }
        public int TipoLancamentoId { get; set; }
        public int? ContaId { get; set; }
        public int UsuarioId { get; set; }

        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime? DataCancelamento { get; set; }
        public string NomeFornecedor { get; set; }
        public string DscTipoPagamento { get; set; }
        public string DscTipoLancamento { get; set; }
        public string NomeUsuario { get; set; }
        public string Banco { get; set; }
        public string Conta { get; set; }
        public string Agencia { get; set; }
        public decimal? Saldo { get; set; }
        public string StatusLancamento { get; set; }  

        public List<ContasDto> ListaContas { get; set; }
        public List<LancamentoDto> ListaLancamento { get; set; }
    }
}
