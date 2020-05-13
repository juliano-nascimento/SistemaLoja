using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loja.Domain.Db
{
   public class Conexao
    {
        private readonly string ConnectionString = "server=localhost;database=dbloja;user id=root;password=123456";

        public MySqlConnection ObterConexao()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
