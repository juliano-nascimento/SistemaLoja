using Microsoft.EntityFrameworkCore.Internal;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Loja.Repository.Dtos
{
    public class UsuarioDto
    {
        public UsuarioDto()
        {

        }

        public UsuarioDto(MySqlDataReader pReader)
        {
            var colunas = pReader.GetColumnSchema();
            if (colunas.Any(c => c.ColumnName.Equals("IdUsuario")) && !string.IsNullOrEmpty(pReader["IdUsuario"].ToString()))
                IdUsuario = (int)pReader["IdUsuario"];
            if (colunas.Any(c => c.ColumnName.Equals("NomeUsuario")) && !string.IsNullOrEmpty(pReader["NomeUsuario"].ToString()))
                NomeUsuario = pReader["NomeUsuario"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("CPF")) && !string.IsNullOrEmpty(pReader["CPF"].ToString()))
                Cpf = pReader["CPF"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("EmailUsuario")) && !string.IsNullOrEmpty(pReader["EmailUsuario"].ToString()))
                EmailUsuario = pReader["EmailUsuario"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("RG")) && !string.IsNullOrEmpty(pReader["RG"].ToString()))
                Rg = pReader["RG"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Rua")) && !string.IsNullOrEmpty(pReader["Rua"].ToString()))
                Rua = pReader["Rua"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Numero")) && !string.IsNullOrEmpty(pReader["Numero"].ToString()))
                Numero = pReader["Numero"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Complemento")) && !string.IsNullOrEmpty(pReader["Complemento"].ToString()))
                Complemento = pReader["Complemento"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Bairro")) && !string.IsNullOrEmpty(pReader["Bairro"].ToString()))
                Bairro = pReader["Bairro"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Cidade")) && !string.IsNullOrEmpty(pReader["Cidade"].ToString()))
                Cidade = pReader["Cidade"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Estado")) && !string.IsNullOrEmpty(pReader["Estado"].ToString()))
                Estado = pReader["Estado"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Senha")) && !string.IsNullOrEmpty(pReader["Senha"].ToString()))
                Senha = pReader["Senha"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Telefone")) && !string.IsNullOrEmpty(pReader["Telefone"].ToString()))
                Telefone = pReader["Telefone"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Celular")) && !string.IsNullOrEmpty(pReader["Celular"].ToString()))
                Celular = pReader["Celular"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("DataCadastro")) && !string.IsNullOrEmpty(pReader["DataCadastro"].ToString()))
                DataCadastro = (DateTime)pReader["DataCadastro"];
            if (colunas.Any(c => c.ColumnName.Equals("NivelUsuario_Id")) && !string.IsNullOrEmpty(pReader["NivelUsuario_Id"].ToString()))
                NivelUsuarioId = (int)pReader["NivelUsuario_Id"];
            if (colunas.Any(c => c.ColumnName.Equals("StatusUsuario_Id")) && !string.IsNullOrEmpty(pReader["StatusUsuario_Id"].ToString()))
                StatusUsuarioId = (int)pReader["StatusUsuario_Id"];
            if (colunas.Any(c => c.ColumnName.Equals("DataExpiracao")) && !string.IsNullOrEmpty(pReader["DataExpiracao"].ToString()))
                DataExpiracao = (DateTime)pReader["DataExpiracao"];

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

        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime DataCadastro { get; set; }
        public int NivelUsuarioId { get; set; }
        public int StatusUsuarioId { get; set; }

        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime DataExpiracao { get; set; }
        public string Mensagem { get; set; }
        public bool Retorno { get; set; }
        public bool Result { get; set; }
    }
}
