using Loja.Domain.Db;
using Loja.Repository.Interfaces;
using Loja.Repository.Models;
using Microsoft.EntityFrameworkCore.Storage;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Repository.Implementations
{
    public class ProdutoRepository : IProdutoRepository
    {
        private StringBuilder stbSQL;
        private readonly MySqlConnection Conn;
        private MySqlCommand command;
        private MySqlDataReader reader;

        public ProdutoRepository()
        {
            Conn = new Conexao().ObterConexao();
        }
        public async Task<bool> Add(Produto produto)
        {
            stbSQL = new StringBuilder();
            bool retorno = false;
            int com;
            try
            {
                stbSQL.Append(" INSERT INTO produto( ");
                stbSQL.Append(" NomeProduto,  ");
                stbSQL.Append(" Unidade,  ");
                stbSQL.Append(" PrecoCompra, ");
                stbSQL.Append(" PrecoVenda,  ");
                stbSQL.Append(" Estoque,   ");
                stbSQL.Append(" EstoqueMinimo, ");
                stbSQL.Append(" Saida,   ");
                stbSQL.Append(" Entrada, ");
                stbSQL.Append(" CodDeBarra, ");
                stbSQL.Append(" DataCadastro, ");
                stbSQL.Append(" DataAtualizacao, ");
                stbSQL.Append(" DataValidade,    ");
                stbSQL.Append(" Usuario_Id)      ");
                stbSQL.Append(" VALUES( ");
                stbSQL.Append($" '{produto.NomeProduto}', ");
                stbSQL.Append($" '{produto.Unidade}', ");
                stbSQL.Append($" '{produto.PrecoCompra}', ");
                stbSQL.Append($" '{produto.PrecoVenda}', ");
                stbSQL.Append($" {produto.Estoque}, ");
                stbSQL.Append($" {produto.EstoqueMinimo}, ");
                if(produto.Saida != null)
                    stbSQL.Append($" {produto.Saida}, ");
                else
                    stbSQL.Append(" null, ");
                if(produto.Entrada != null)
                    stbSQL.Append($" {produto.Entrada}, ");
                else
                    stbSQL.Append(" null, ");
                stbSQL.Append($" '{produto.CodDeBarra}', ");
                stbSQL.Append($" '{DateTime.Now.ToString("yyyy/MM/dd HH:MM:ss")}', ");
                stbSQL.Append($" '{DateTime.Now.ToString("yyyy/MM/dd HH:MM:ss")}', ");
                if(produto.DataValidade != null)
                    stbSQL.Append($" '{produto.DataValidade}', ");
                else
                    stbSQL.Append(" null, ");
                stbSQL.Append($" {produto.UsuarioId}) ");
                command = new MySqlCommand(stbSQL.ToString(), Conn);
                await Conn.OpenAsync();
                com = await command.ExecuteNonQueryAsync();
                if (com > 0)
                    retorno = true;
                await Conn.CloseAsync();
            }
            catch (Exception ex)
            {
                await Conn.CloseAsync();
                throw new Exception("Erro ao adicionar produto. " + ex.Message);
            }
            return retorno;
        }

        public async Task<List<ProdutoModel>> FindAll()
        {
            List<ProdutoModel> lstProduto = new List<ProdutoModel>();
            ProdutoModel produto = new ProdutoModel();
            stbSQL = new StringBuilder();
            try
            {
                stbSQL.Append(" SELECT  ");
                stbSQL.Append(" IdProduto, ");
                stbSQL.Append(" CodDeBarra, ");
                stbSQL.Append(" NomeProduto, ");
                stbSQL.Append(" PrecoVenda,  ");
                stbSQL.Append(" Estoque  ");
                stbSQL.Append(" FROM produto NOLOCK ");
                stbSQL.Append(" ORDER BY IdProduto ");
                stbSQL.Append(" LIMIT 10  ");
                command = new MySqlCommand(stbSQL.ToString(), Conn);
                await Conn.OpenAsync();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lstProduto.Add(new ProdutoModel(reader));
                }
                reader.Close();
                await Conn.CloseAsync();
            }
            catch (Exception ex)
            {
                reader.Close();
                await Conn.CloseAsync();
                throw new Exception("Erro ao consultar todos os produtos. " + ex.Message);
            }
            return lstProduto;
        }

        public async Task<ProdutoModel> FindById(int id)
        {
            ProdutoModel produto = new ProdutoModel();
            stbSQL = new StringBuilder();
            try
            {
                stbSQL.Append(" SELECT            ");
                stbSQL.Append(" p.IdProduto,      ");
                stbSQL.Append(" p.NomeProduto,    ");
                stbSQL.Append(" p.Unidade,        ");
                stbSQL.Append(" p.PrecoCompra,    ");
                stbSQL.Append(" p.PrecoVenda,     ");
                stbSQL.Append(" p.Estoque,        ");
                stbSQL.Append(" p.EstoqueMinimo,  ");
                stbSQL.Append(" CASE WHEN p.Saida is null THEN 'Não' ELSE 'Sim' END AS RetSaida, ");
                stbSQL.Append(" CASE WHEN p.Entrada is null THEN 'Não' ELSE 'Sim' END AS RetEntrada, ");
                stbSQL.Append(" p.CodDeBarra,      ");
                stbSQL.Append(" p.DataCadastro,    ");
                stbSQL.Append(" p.DataAtualizacao, ");
                stbSQL.Append(" p.DataValidade,    ");
                stbSQL.Append(" u.NomeUsuario      ");
                stbSQL.Append(" FROM produto p     ");
                stbSQL.Append(" JOIN usuario u ON p.Usuario_Id = u.IdUsuario ");
                stbSQL.Append($" WHERE p.IdProduto = {id}  ");
                command = new MySqlCommand(stbSQL.ToString(), Conn);
                await Conn.OpenAsync();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    produto = new ProdutoModel(reader);
                }
                reader.Close();
                await Conn.CloseAsync();
            }
            catch(Exception ex)
            {
                reader.Close();
                await Conn.CloseAsync();
                throw new Exception("Erro ao consultar o produto por código. " + ex.Message);
            }
            return produto;
        }

        public async Task<bool> Update(ProdutoModel pProduto)
        {
            stbSQL = new StringBuilder();
            bool retorno = false;
            int com;
            try
            {
                stbSQL.Append(" UPDATE produto          ");
                stbSQL.Append($" SET CodDeBarra = '{pProduto.CodDeBarra}' , ");
                stbSQL.Append($"     NomeProduto = '{pProduto.NomeProduto}', ");
                if(pProduto.Entrada != null)
                    stbSQL.Append($"     Entrada = {pProduto.Entrada}, ");
                if(pProduto.Saida != null)
                    stbSQL.Append($"     Saida = {pProduto.Saida}, ");
                stbSQL.Append($"     PrecoCompra = '{pProduto.PrecoCompra}', ");
                stbSQL.Append($"     PrecoVenda = '{pProduto.PrecoVenda}', ");
                stbSQL.Append($"     Unidade = '{pProduto.Unidade}', ");
                stbSQL.Append($"     Estoque = {pProduto.Estoque}, ");
                stbSQL.Append($"     EstoqueMinimo = {pProduto.EstoqueMinimo}, ");
                if(pProduto.DataValidadeEd != null)
                    stbSQL.Append($"     DataValidade = '{pProduto.DataValidadeEd}', ");
                stbSQL.Append($"     DataAtualizacao = '{DateTime.Now.ToString("yyyy/MM/dd HH:MM:ss")}', ");
                stbSQL.Append($"     Usuario_Id = {pProduto.UsuarioId} ");
                stbSQL.Append($" WHERE IdProduto =  {pProduto.IdProduto} ");
                command = new MySqlCommand(stbSQL.ToString(), Conn);
                await Conn.OpenAsync();
                com = await command.ExecuteNonQueryAsync();
                if (com > 0)
                    retorno = true;
                await Conn.CloseAsync();
            }
            catch(Exception ex)
            {
                await Conn.CloseAsync();
                throw new Exception("Erro ao atualizar o produto. " + ex.Message);
            }
            return retorno;
        }

        public async Task<bool> Delete(int pId)
        {
            stbSQL = new StringBuilder();
            bool retorno = false;
            int com;
            try
            {
                stbSQL.Append(" DELETE ");
                stbSQL.Append(" FROM Produto ");
                stbSQL.Append(" WHERE ");
                stbSQL.Append($" IdProduto = {pId} ");
                command = new MySqlCommand(stbSQL.ToString(), Conn);
                await Conn.OpenAsync();
                com = await command.ExecuteNonQueryAsync();
                if (com > 0)
                    retorno = true;
                await Conn.CloseAsync();
            }
            catch(Exception ex)
            {
                await Conn.CloseAsync();
                throw new Exception("Erro ao deletar o Produto. " + ex.Message);
            }
            return retorno;
        }

        public async Task<bool> UpdateEstoque(int pId, int pEstoque)
        {
            stbSQL = new StringBuilder();
            bool result = false;
            int com;
            try
            {
                stbSQL.Append("UPDATE Produto ");
                stbSQL.Append($" SET Estoque = {pEstoque} ");
                stbSQL.Append($" WHERE IdProduto = {pId}");
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
                throw new Exception("Erro ao atualizar estoque. " + ex.Message);
            }
            return result;
        }
    }
}
