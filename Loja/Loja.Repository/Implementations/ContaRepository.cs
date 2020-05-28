using Loja.Domain.Db;
using Loja.Repository.Dtos;
using Loja.Repository.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Repository.Implementations
{
    public class ContaRepository : IContaRepository
    {
        private StringBuilder stbSQL;
        private readonly MySqlConnection Conn;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public ContaRepository()
        {
            Conn = new Conexao().ObterConexao();
        }

        public async Task<List<ContasDto>> FindAll()
        {
            stbSQL = new StringBuilder();
            List<ContasDto> lstContas = new List<ContasDto>();
            try
            {
                stbSQL.Append(" SELECT ");
                stbSQL.Append(" * ");
                stbSQL.Append(" FROM contas NOLOCK ");
                stbSQL.Append(" ORDER BY IdConta ");
                command = new MySqlCommand(stbSQL.ToString(), Conn);
                await Conn.OpenAsync();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lstContas.Add(new ContasDto(reader));
                }
                reader.Close();
                await Conn.CloseAsync();
            }
            catch(Exception ex)
            {
                reader.Close();
                await Conn.CloseAsync();
                throw new Exception("Erro ao consultar Contas cadastradas. " + ex.Message);
            }
            return lstContas;
        }
    }
}
