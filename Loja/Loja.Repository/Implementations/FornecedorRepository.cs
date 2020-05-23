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
    public class FornecedorRepository : IFornecedorRepository
    {
        private StringBuilder stbSQL;
        private readonly MySqlConnection Conn;
        private MySqlCommand command;
        private MySqlDataReader reader;
        private readonly dblojaContext _context;

        public FornecedorRepository(dblojaContext context)
        {
            _context = context;
            Conn = new Conexao().ObterConexao();
        }

        public async Task<List<FornecedorDto>> FindByAtivos(int pStatus)
        {
            stbSQL = new StringBuilder();
            List<FornecedorDto> lstFornecedores = new List<FornecedorDto>();
            try
            {
                stbSQL.Append(" SELECT  ");
                stbSQL.Append(" *       ");
                stbSQL.Append(" FROM fornecedor NOLOCK  ");
                stbSQL.Append($" WHERE StatusFornecedor_Id = {pStatus}  ");
                stbSQL.Append("ORDER BY IdFornecedor ");
                stbSQL.Append(" LIMIT 0,10   ");
                command = new MySqlCommand(stbSQL.ToString(), Conn);
                await Conn.OpenAsync();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lstFornecedores.Add(new FornecedorDto(reader));
                }
                reader.Close();
                await Conn.CloseAsync();
            }
            catch (Exception ex)
            {
                reader.Close();
                await Conn.CloseAsync();
                throw new Exception("Erro ao consultar os fornecedores ativos. " + ex.Message);
            }
            return lstFornecedores;
        }

        public async Task<bool> Add(Fornecedor fornecedor)
        {
            stbSQL = new StringBuilder();
            bool result = false;
            int com;
            try
            {
                stbSQL.Append(" INSERT INTO Fornecedor( ");
                stbSQL.Append(" NomeFornecedor, ");
                stbSQL.Append(" CNPJ,      ");
                stbSQL.Append(" Telefone,  ");
                stbSQL.Append(" Celular,   ");
                stbSQL.Append(" StatusFornecedor_Id)  ");
                stbSQL.Append(" VALUES( ");
                stbSQL.Append($" '{fornecedor.NomeFornecedor}', ");
                stbSQL.Append($" '{fornecedor.Cnpj}', ");
                stbSQL.Append($" '{fornecedor.Telefone}', ");
                stbSQL.Append($" '{fornecedor.Celular}', ");
                stbSQL.Append($" {fornecedor.StatusFornecedorId}) ");
                command = new MySqlCommand(stbSQL.ToString(), Conn);
                await Conn.OpenAsync();
                com = await command.ExecuteNonQueryAsync();
                if (com > 0)
                    result = true;
                await Conn.CloseAsync();
            }
            catch (Exception ex)
            {
                await Conn.CloseAsync();
                throw new Exception("Erro ao cadastrar fornecedor. " + ex.Message);
            }
            return result;
        }

        public async Task<FornecedorDto> FindById(int pId)
        {
            stbSQL = new StringBuilder();
            FornecedorDto fornecedor = new FornecedorDto();
            try
            {
                stbSQL.Append(" SELECT ");
                stbSQL.Append(" f.IdFornecedor, ");
                stbSQL.Append(" f.NomeFornecedor, ");
                stbSQL.Append(" f.CNPJ, ");
                stbSQL.Append(" f.Telefone, ");
                stbSQL.Append(" f.Celular, ");
                stbSQL.Append(" f.StatusFornecedor_Id, ");
                stbSQL.Append(" sf.DscStatusFornecedor ");
                stbSQL.Append(" FROM fornecedor f ");
                stbSQL.Append(" JOIN status_fornecedor sf ON f.StatusFornecedor_Id = sf.IdStatusFornecedor ");
                stbSQL.Append($" WHERE f.IdFornecedor = {pId} ");
                command = new MySqlCommand(stbSQL.ToString(), Conn);
                await Conn.OpenAsync();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    fornecedor = new FornecedorDto(reader);
                }
                reader.Close();
                await Conn.CloseAsync();
            }
            catch (Exception ex)
            {
                reader.Close();
                await Conn.CloseAsync();
                throw new Exception("Erro ao consultar fornecedor por código. " + ex.Message);
            }
            return fornecedor;
        }

        public async Task<bool> Delete(int pId)
        {
            stbSQL = new StringBuilder();
            bool result = false;
            int com;
            try
            {
                stbSQL.Append(" DELETE ");
                stbSQL.Append(" FROM Fornecedor ");
                stbSQL.Append(" WHERE ");
                stbSQL.Append($" IdFornecedor = {pId} ");
                command = new MySqlCommand(stbSQL.ToString(), Conn);
                await Conn.OpenAsync();
                com = await command.ExecuteNonQueryAsync();
                if (com > 0)
                    result = true;
                await Conn.CloseAsync();
            }
            catch (Exception ex)
            {
                await Conn.CloseAsync();
                throw new Exception("Erro ao deletar fornecedor. " + ex.Message);
            }
            return result;
        }

        public async Task<bool> Update(Fornecedor pFornecedor)
        {
            stbSQL = new StringBuilder();
            bool result = false;
            int com;
            try
            {
                stbSQL.Append(" UPDATE fornecedor       ");
                stbSQL.Append($"  SET NomeFornecedor = '{pFornecedor.NomeFornecedor}',  ");
                stbSQL.Append($"      Telefone = '{pFornecedor.Telefone}', ");
                stbSQL.Append($"      Celular = '{pFornecedor.Celular}', ");
                stbSQL.Append($"      StatusFornecedor_Id = {pFornecedor.StatusFornecedorId} ");
                stbSQL.Append($" WHERE IdFornecedor = {pFornecedor.IdFornecedor} ");
                command = new MySqlCommand(stbSQL.ToString(), Conn);
                await Conn.OpenAsync();
                com = await command.ExecuteNonQueryAsync();
                if (com > 0)
                    result = true;
                await Conn.CloseAsync();
            }
            catch (Exception ex)
            {
                await Conn.CloseAsync();
                throw new Exception("Erro ao atualizar o Fornecedor. " + ex.Message);
            }
            return result;
        }

        public async Task<List<string>> FindNomeIdPorTermo(string pTermo)
        {
            stbSQL = new StringBuilder();
            List<string> lstFornecedores = new List<string>();
            try
            {
                stbSQL.Append(" SELECT   ");
                stbSQL.Append(" IdFornecedor, ");
                stbSQL.Append(" NomeFornecedor ");
                stbSQL.Append(" FROM fornecedor NOLOCK  ");
                stbSQL.Append($" WHERE NomeFornecedor LIKE '{pTermo}%' ");
                command = new MySqlCommand(stbSQL.ToString(), Conn);
                await Conn.OpenAsync();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lstFornecedores.Add(string.Format("{0}-{1}", reader["NomeFornecedor"], reader["IdFornecedor"]));
                }
                reader.Close();
                await Conn.CloseAsync();
            }
            catch(Exception ex)
            {
                reader.Close();
                await Conn.CloseAsync();
                throw new Exception("Erro ao consultar fornecedor por termo. " + ex.Message);
            }
            return lstFornecedores;
        }
    }
}
