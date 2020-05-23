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
    public class PedidoRepository : IPedidoRepository
    {
        private StringBuilder stbSQL;
        private readonly MySqlConnection Conn;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public PedidoRepository()
        {
            Conn = new Conexao().ObterConexao();
        }

        public async Task<List<PedidoDto>> FindAllPedidos()
        {
            stbSQL = new StringBuilder();
            List<PedidoDto> lstPedido = new List<PedidoDto>();
            try
            {
                stbSQL.Append(" SELECT  ");
                stbSQL.Append(" p.IdPedido, ");
                stbSQL.Append(" p.DescricaoPedido, ");
                stbSQL.Append(" p.DataPedido,  ");
                stbSQL.Append(" p.ValorTotal,  ");
                stbSQL.Append(" p.DscProdutos, ");
                stbSQL.Append(" p.Usuario_Id,  ");
                stbSQL.Append(" u.NomeUsuario, ");
                stbSQL.Append(" p.StatusPedido_Id, ");
                stbSQL.Append(" sp.DscStatusPedido, ");
                stbSQL.Append(" p.Fornecedor_Id, ");
                stbSQL.Append(" f.NomeFornecedor ");
                stbSQL.Append(" FROM pedido p  ");
                stbSQL.Append(" JOIN usuario u ON p.Usuario_Id = u.IdUsuario ");
                stbSQL.Append(" JOIN status_pedido sp ON p.StatusPedido_Id = sp.IdStatusPedido ");
                stbSQL.Append(" JOIN fornecedor f ON p.Fornecedor_Id = f.IdFornecedor ");
                stbSQL.Append(" ORDER BY p.IdPedido ");
                command = new MySqlCommand(stbSQL.ToString(), Conn);
                await Conn.OpenAsync();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lstPedido.Add(new PedidoDto(reader));
                }
                reader.Close();
                await Conn.CloseAsync();
            }
            catch (Exception ex)
            {
                reader.Close();
                await Conn.CloseAsync();
                throw new Exception("Erro ao consultar todos os pedidos. " + ex.Message);
            }
            return lstPedido;
        }

        public async Task<bool> Add(Pedido pPedido)
        {
            stbSQL = new StringBuilder();
            bool result = false;
            int com;
            try
            {
                stbSQL.Append(" INSERT INTO pedido( ");
                stbSQL.Append(" DescricaoPedido, ");
                stbSQL.Append(" DataPedido,  ");
                stbSQL.Append(" ValorTotal,  ");
                stbSQL.Append(" DscProdutos, ");
                stbSQL.Append(" Usuario_Id,  ");
                stbSQL.Append(" StatusPedido_Id, ");
                stbSQL.Append(" Lancamento_Id, ");
                stbSQL.Append(" Fornecedor_Id) ");
                stbSQL.Append(" VALUES(  ");
                stbSQL.Append($" '{pPedido.DescricaoPedido}', ");
                stbSQL.Append($" '{DateTime.Now:yyyy/MM/dd HH:MM:ss}', ");
                stbSQL.Append($" '{pPedido.ValorTotal}', ");
                stbSQL.Append($" '{pPedido.DscProdutos}', ");
                stbSQL.Append($" {pPedido.UsuarioId}, ");
                stbSQL.Append($" {pPedido.StatusPedidoId}, ");
                stbSQL.Append($" {pPedido.LancamentoId}, ");
                stbSQL.Append($" {pPedido.FornecedorId}) ");
                command = new MySqlCommand(stbSQL.ToString(), Conn);
                await Conn.OpenAsync();
                com = await command.ExecuteNonQueryAsync();
                if (com > 0)
                    result = true;
                await Conn.CloseAsync();

            }
            catch(Exception ex)
            {
                await Conn.CloseAsync();
                throw new Exception("Erro ao cadastrar pedido. " + ex.Message);
            }
            return result;
        }

        public async Task<PedidoDto> FindById(int pIdPedido)
        {
            stbSQL = new StringBuilder();
            PedidoDto pedido = new PedidoDto();
            try
            {
                stbSQL.Append(" SELECT  ");
                stbSQL.Append(" p.IdPedido, ");
                stbSQL.Append(" p.DescricaoPedido, ");
                stbSQL.Append(" p.DataPedido, ");
                stbSQL.Append(" p.ValorTotal, ");
                stbSQL.Append(" p.DscProdutos, ");
                stbSQL.Append(" u.NomeUsuario, ");
                stbSQL.Append("p.StatusPedido_Id, ");
                stbSQL.Append(" sp.DscStatusPedido, ");
                stbSQL.Append(" f.NomeFornecedor  ");
                stbSQL.Append(" FROM pedido p  ");
                stbSQL.Append(" JOIN usuario u ON p.Usuario_Id = u.IdUsuario ");
                stbSQL.Append(" JOIN status_pedido sp ON p.StatusPedido_Id = sp.IdStatusPedido  ");
                stbSQL.Append(" JOIN fornecedor f ON p.Fornecedor_Id = f.IdFornecedor ");
                stbSQL.Append($" WHERE p.IdPedido = {pIdPedido}");
                command = new MySqlCommand(stbSQL.ToString(), Conn);
                await Conn.OpenAsync();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    pedido = new PedidoDto(reader);
                }
                reader.Close();
                await Conn.CloseAsync();
            }
            catch(Exception ex)
            {
                reader.Close();
                await Conn.CloseAsync();
                throw new Exception("Erro ao consultar pedido por código. " + ex.Message);
            }
            return pedido;
        }

        public async Task<bool> Update(PedidoDto pPedido)
        {
            stbSQL = new StringBuilder();
            bool result = false;
            int com;
            try
            {
                stbSQL.Append(" UPDATE pedido  ");
                stbSQL.Append($" SET DescricaoPedido = '{pPedido.DescricaoPedido}', ");
                stbSQL.Append($" ValorTotal = '{pPedido.ValorTotal}', ");
                stbSQL.Append($" DscProdutos = '{pPedido.DscProdutos}', ");
                stbSQL.Append($" StatusPedido_Id = {pPedido.StatusPedidoId} ");
                stbSQL.Append($" WHERE IdPedido = {pPedido.IdPedido} ");
                command = new MySqlCommand(stbSQL.ToString(), Conn);
                await Conn.OpenAsync();
                com = command.ExecuteNonQuery();
                if (com > 0)
                    result = true;
                await Conn.CloseAsync();
            }
            catch(Exception ex)
            {
                await Conn.CloseAsync();
                throw new Exception("Erro ao atualizar o pedido. " + ex.Message);
            }
            return result;
        }

        public async Task<bool> Delete(int pIdPedido)
        {
            stbSQL = new StringBuilder();
            bool result = false;
            int com;
            try
            {
                stbSQL.Append("DELETE ");
                stbSQL.Append(" FROM pedido ");
                stbSQL.Append($" WHERE IdPedido = {pIdPedido} ");
                command = new MySqlCommand(stbSQL.ToString(), Conn);
                await Conn.OpenAsync();
                com = command.ExecuteNonQuery();
                if (com > 0)
                    result = true;
                await Conn.CloseAsync();
            }
            catch(Exception ex)
            {
                await Conn.CloseAsync();
                throw new Exception("Erro ao excluir o pedido. " + ex.Message);
            }
            return result;
        }
    }
}
