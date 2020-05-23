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
    public class LancamentoRepository : ILancamentoRepository
    {
        private StringBuilder stbSQL;
        private readonly MySqlConnection Conn;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public LancamentoRepository()
        {
            Conn = new Conexao().ObterConexao();
        }

        public async Task<bool> Add(Lancamento pLancamento)
        {
            stbSQL = new StringBuilder();
            bool result = false;
            int com;
            try
            {
                stbSQL.Append(" INSERT INTO lancamento( ");
                stbSQL.Append(" DscLancamento,   ");
                stbSQL.Append(" DataLancamento, ");
                stbSQL.Append(" ValorLancamento,  ");
                stbSQL.Append(" DataVencimento,  ");
                stbSQL.Append(" DataPagamento,  ");
                stbSQL.Append(" Baixado,  ");
                stbSQL.Append(" Fornecedor_Id, ");
                stbSQL.Append(" TipoPagamento_Id, ");
                stbSQL.Append(" TipoLancamento_Id, ");
                stbSQL.Append(" Conta_Id,  ");
                stbSQL.Append(" Usuario_Id,  ");
                stbSQL.Append(" DataCancelamento) ");
                stbSQL.Append(" VALUES(  ");
                stbSQL.Append($" '{pLancamento.DscLancamento}', ");
                stbSQL.Append($" '{DateTime.Now.ToString("yyyy/MM/dd HH:MM:ss")}', ");
                stbSQL.Append($" '{pLancamento.ValorLancamento}', ");
                if (!string.IsNullOrEmpty(pLancamento.DataVencimento.ToString()))
                    stbSQL.Append($" '{pLancamento.DataVencimento}', ");
                else
                    stbSQL.Append(" null, ");
                if (!string.IsNullOrEmpty(pLancamento.DataPagamento.ToString()))
                    stbSQL.Append($" '{pLancamento.DataPagamento}', ");
                else
                    stbSQL.Append(" null, ");
                if (!string.IsNullOrEmpty(pLancamento.Baixado.ToString()) && (pLancamento.Baixado == 0 || pLancamento.Baixado == 1))
                    stbSQL.Append($" {pLancamento.Baixado}, ");
                else
                    stbSQL.Append(" null, ");
                stbSQL.Append($" {pLancamento.FornecedorId}, ");
                stbSQL.Append($" {pLancamento.TipoPagamentoId}, ");
                stbSQL.Append($" {pLancamento.TipoLancamentoId}, ");
                if (!string.IsNullOrEmpty(pLancamento.ContaId.ToString()) && pLancamento.ContaId != 0)
                    stbSQL.Append($" {pLancamento.ContaId}, ");
                else
                    stbSQL.Append(" null, ");
                stbSQL.Append($" {pLancamento.UsuarioId}, ");
                if (!string.IsNullOrEmpty(pLancamento.DataCancelamento.ToString()))
                    stbSQL.Append($" '{pLancamento.DataCancelamento}') ");
                else
                    stbSQL.Append(" null) ");
                command = new MySqlCommand(stbSQL.ToString(), Conn);
                await Conn.OpenAsync();
                com = command.ExecuteNonQuery();
                if (com > 0)
                    result = true;
                await Conn.CloseAsync();
            }
            catch (Exception ex)
            {
                await Conn.CloseAsync();
                throw new Exception("Erro ao cadastrar lancamento financeiro. " + ex.Message);
            }
            return result;
        }

        public async Task<List<LancamentoDto>> FindIdLancamento(int pUsuarioId, int pTipoLancamentoId)
        {
            stbSQL = new StringBuilder();
            List<LancamentoDto> lstLancamentos = new List<LancamentoDto>();
            try
            {
                stbSQL.Append(" SELECT    ");
                stbSQL.Append(" IdLancamento  ");
                stbSQL.Append(" FROM lancamento  ");
                stbSQL.Append($" WHERE Usuario_Id = {pUsuarioId} ");
                stbSQL.Append($" AND TipoLancamento_Id = {pTipoLancamentoId} ");
                stbSQL.Append("ORDER BY IdLancamento DESC ");
                command = new MySqlCommand(stbSQL.ToString(), Conn);
                await Conn.OpenAsync();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lstLancamentos.Add(new LancamentoDto
                    {
                        IdLancamento = (int)reader["IdLancamento"]
                    });
                }
                reader.Close();
                await Conn.CloseAsync();
            }
            catch(Exception ex)
            {
                reader.Close();
                await Conn.CloseAsync();
                throw new Exception("Erro ao consultar o codigo do lancamento. " + ex.Message);
            }
            return lstLancamentos;
        }

        public void DesfazerLancamento(int pId)
        {
            stbSQL = new StringBuilder();
            try
            {
                stbSQL.Append("DELETE ");
                stbSQL.Append(" FROM lancamento ");
                stbSQL.Append(" WHERE ");
                stbSQL.Append($" IdLancamento = {pId} ");
                command = new MySqlCommand(stbSQL.ToString(), Conn);
                Conn.Open();
                command.ExecuteNonQuery();
                Conn.Close();
            }
            catch(Exception ex)
            {
                Conn.Close();
                throw new Exception("Erro ao desfazer lancamento. " + ex.Message);
            }
            
        }
    }
}
