using Loja.Domain.Db;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

namespace Loja.Repository.Dtos
{
    public class FornecedorDto
    {
        public FornecedorDto(MySqlDataReader pReader)
        {
            //pega todas as colunas que retornaram no reader
            var colunas = pReader.GetColumnSchema();
            //verifica se a coluna existe no reader, para poder atribuir a propriedade da classe
            if (colunas.Any(c => c.ColumnName.Equals("IdFornecedor")) && !string.IsNullOrEmpty(pReader["IdFornecedor"].ToString()))
                IdFornecedor = (int)pReader["IdFornecedor"];
            if (colunas.Any(c => c.ColumnName.Equals("NomeFornecedor")) && !string.IsNullOrEmpty(pReader["NomeFornecedor"].ToString()))
                NomeFornecedor = pReader["NomeFornecedor"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("CNPJ")) && !string.IsNullOrEmpty(pReader["CNPJ"].ToString()))
                Cnpj = pReader["CNPJ"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Telefone")) && !string.IsNullOrEmpty(pReader["Telefone"].ToString()))
                Telefone = pReader["Telefone"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("Celular")) && !string.IsNullOrEmpty(pReader["Celular"].ToString()))
                Celular = pReader["Celular"].ToString();
            if (colunas.Any(c => c.ColumnName.Equals("StatusFornecedor_Id")) && !string.IsNullOrEmpty(pReader["StatusFornecedor_Id"].ToString()))
                StatusFornecedorId = (int)pReader["StatusFornecedor_Id"];
            if (colunas.Any(c => c.ColumnName.Equals("DscStatusFornecedor")) && !string.IsNullOrEmpty(pReader["DscStatusFornecedor"].ToString()))
                DscStatusFornecedor = pReader["DscStatusFornecedor"].ToString();
        }
        public FornecedorDto()
        {

        }
        public int IdFornecedor { get; set; }
        public string NomeFornecedor { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public int StatusFornecedorId { get; set; }
        public string DscStatusFornecedor { get; set; }
        public StatusFornecedor StatusFornecedor { get; set; }
        public List<FornecedorDto> ListaFornecedores { get; set; }
    }
}
