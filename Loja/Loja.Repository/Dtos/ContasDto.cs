using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loja.Repository.Dtos
{
    public class ContasDto
    {
        public ContasDto()
        {

        }

        public ContasDto(MySqlDataReader pReader)
        {
            var colunas = pReader.GetColumnSchema();

            if (colunas.Any(c => c.ColumnName.Equals("IdConta")) && !string.IsNullOrEmpty(pReader["IdConta"].ToString()))
                IdConta = (int)pReader["IdConta"];
            if (colunas.Any(c => c.ColumnName.Equals("Banco")) && !string.IsNullOrEmpty(pReader["Banco"].ToString()))
                Banco = pReader["Banco"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Conta")) && !string.IsNullOrEmpty(pReader["Conta"].ToString()))
                Conta = pReader["Conta"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Agencia")) && !string.IsNullOrEmpty(pReader["Agencia"].ToString()))
                Agencia = pReader["Agencia"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Saldo")) && !string.IsNullOrEmpty(pReader["Saldo"].ToString()))
                Saldo = pReader["Saldo"].ToString();
        }
        public int IdConta { get; set; }
        public string Banco { get; set; }
        public string Conta { get; set; }
        public string Agencia { get; set; }
        public string Saldo { get; set; }
    }
}
