using GestaoProcessosBDPOO.Interfaces;
using GestaoProcessosBDPOO.Models;
using System;
using System.Collections.Generic;
using System.Text;
using GestaoProcessosBDPOO.Utilitarios;
using MySql.Data.MySqlClient;

namespace GestaoProcessosBDPOO.Dao
{
    internal class MovimentacaoDAO:IMovimentacaoDao
    {
        public void Insert(Movimentacao movimentacao)
        {
            try
            {
                string sql = @"INSERT INTO MOVIMENTACAO (dataHora, descricao, FK_processoID, FK_usuarioID)
                               VALUES (@dataHora, @descricao, @FK_processoID, @FK_usuarioID)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@dataHora", movimentacao.DataHora);
                    if (movimentacao.Descricao != null)
                        cmd.Parameters.AddWithValue("@descricao", movimentacao.Descricao);
                    else
                        cmd.Parameters.AddWithValue("@descricao", DBNull.Value);

                    cmd.Parameters.AddWithValue("@FK_processoID", movimentacao.FK_processoID);
                    cmd.Parameters.AddWithValue("@FK_usuarioID", movimentacao.FK_usuarioID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Movimentacao movimentacao)
        {
            try
            {
                string sql = @"UPDATE MOVIMENTACAO SET dataHora = @dataHora, descricao = @descricao, FK_processoID = @processoID, FK_usuarioID = @usuarioID
                               WHERE movimentacaoID = @movimentacaoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@dataHora", movimentacao.DataHora);
                    if (movimentacao.Descricao != null)
                        cmd.Parameters.AddWithValue("@descricao", movimentacao.Descricao);
                    else
                        cmd.Parameters.AddWithValue("@descricao", DBNull.Value);

                    cmd.Parameters.AddWithValue("@processoID", movimentacao.FK_processoID);
                    cmd.Parameters.AddWithValue("@usuarioID", movimentacao.FK_usuarioID);
                    cmd.Parameters.AddWithValue("@movimentacaoID", movimentacao.MovimentacaoID);
                    var linhas = cmd.ExecuteNonQuery();
                    if (linhas == 0)
                        throw new Exception("Nenhum registro foi atualizado, verifique o movimentacaoID.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Delete(int id)
        {
            try
            {
                string sql = "DELETE FROM MOVIMENTACAO WHERE movimentacaoID = @movimentacaoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@movimentacaoID", id);
                    var linhasAfetadas = cmd.ExecuteNonQuery();
                    if (linhasAfetadas == 0)
                        throw new Exception("Nenhum registro encontrado com esse ID.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Movimentacao> GetAll()
        {
            List<Movimentacao> movimentacoes = new List<Movimentacao>();
            try
            {
                string sql = "SELECT * FROM MOVIMENTACAO ORDER BY dataHora DESC, movimentacaoID DESC";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Movimentacao movimentacao = new Movimentacao
                        {
                            MovimentacaoID = Convert.ToInt32(dr["movimentacaoID"]),
                            DataHora = Convert.ToDateTime(dr["dataHora"]),
                            FK_processoID = Convert.ToInt32(dr["FK_processoID"]),
                            FK_usuarioID = Convert.ToInt32(dr["FK_usuarioID"])
                        };

                        if (dr["descricao"] != DBNull.Value)
                            movimentacao.Descricao = dr["descricao"].ToString();
                        movimentacoes.Add(movimentacao);
                    }
                }

                return movimentacoes;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os movimentações: {ex.Message}");
            }
        }
        public Movimentacao BuscarID(int movimentacaoID)
        {
            try
            {
                string sql = "SELECT * FROM MOVIMENTACAO WHERE movimentacaoID = @movimentacaoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@movimentacaoID", movimentacaoID);
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Movimentacao movimentacao = new Movimentacao
                            {
                                MovimentacaoID = Convert.ToInt32(dr["movimentacaoID"]),
                                DataHora = Convert.ToDateTime(dr["dataHora"]),
                                FK_processoID = Convert.ToInt32(dr["FK_processoID"]),
                                FK_usuarioID = Convert.ToInt32(dr["FK_usuarioID"])
                            };

                            if (dr["descricao"] != DBNull.Value)
                                movimentacao.Descricao = dr["descricao"].ToString();
                            return movimentacao;
                        }
                        else
                        {
                            throw new Exception("Nenhum movimentação encontrado com esse ID.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
