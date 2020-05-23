using Loja.Domain.Db;
using Loja.Repository.Dtos;
using Loja.Repository.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Repository.Implementations
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private StringBuilder stbSQL;
        private readonly MySqlConnection Conn;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public UsuarioRepository()
        {
            Conn = new Conexao().ObterConexao();
        }
        public async Task<UsuarioDto> FindByEmailAsync(string Email)
        {
            UsuarioDto model = new UsuarioDto();
            stbSQL = new StringBuilder();
            try
            {
                stbSQL.Append(" SELECT   ");
                stbSQL.Append(" IdUsuario, ");
                stbSQL.Append(" NomeUsuario, ");
                stbSQL.Append(" CPF,  ");
                stbSQL.Append(" EmailUsuario, ");
                stbSQL.Append(" Senha, ");
                stbSQL.Append(" NivelUsuario_Id,  ");
                stbSQL.Append(" StatusUsuario_Id, ");
                stbSQL.Append(" DataExpiracao   ");
                stbSQL.Append(" FROM usuario NOLOCK  ");
                stbSQL.Append($" WHERE EmailUsuario = '{Email}' ");
                command = new MySqlCommand(stbSQL.ToString(), Conn);
                await Conn.OpenAsync();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    model = new UsuarioDto(reader);
                }
                reader.Close();
                await Conn.CloseAsync();
            }
            catch (Exception ex)
            {
                reader.Close();
                await Conn.CloseAsync();
                throw new Exception("Erro ao obter usuario por email. " + ex.Message);
            }
            return model;
        }
    }
}
