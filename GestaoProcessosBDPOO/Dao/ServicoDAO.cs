using GestaoProcessosBDPOO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using GestaoProcessosBDPOO.Models;
using GestaoProcessosBDPOO.Utilitarios;
using MySql.Data.MySqlClient;

namespace GestaoProcessosBDPOO.Dao
{
    internal class ServicoDAO:IServicoDao
    {
        public void Insert(Servico servico)
        {
            try
            {
                string sql = @"INSERT INTO SERVICO (nome, descricao, ativo)
                               VALUES (@nome, @descricao, @ativo)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", servico.Nome);

                    if (servico.Descricao != null)
                        cmd.Parameters.AddWithValue("@descricao", servico.Descricao);
                    else
                        cmd.Parameters.AddWithValue("@descricao", DBNull.Value);

                    cmd.Parameters.AddWithValue("@ativo", servico.Ativo);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Servico servico)
        {
            try
            {
                string sql = @"UPDATE SERVICO SET nome = @nome, descricao = @descricao, ativo = @ativo WHERE servicoID = @servicoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", servico.Nome);

                    if (servico.Descricao != null)
                        cmd.Parameters.AddWithValue("@descricao", servico.Descricao);
                    else
                        cmd.Parameters.AddWithValue("@descricao", DBNull.Value);

                    cmd.Parameters.AddWithValue("@ativo", servico.Ativo);
                    cmd.Parameters.AddWithValue("@servicoID", servico.ServicoID);
                    var linhas = cmd.ExecuteNonQuery();
                    if (linhas == 0)
                        throw new Exception("Nenhum registro foi atualizado, verifique o servicoID.");
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
                string sql = "DELETE FROM SERVICO WHERE servicoID = @servicoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@servicoID", id);
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
        public List<Servico> GetAll()
        {
            List<Servico> servicos = new List<Servico>();

            try
            {
                string sql = "SELECT * FROM SERVICO ORDER BY nome";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Servico servico = new Servico
                        {
                            ServicoID = Convert.ToInt32(dr["servicoID"]),
                            Nome = dr["nome"].ToString() ?? string.Empty,
                            Ativo = Convert.ToBoolean(dr["ativo"])
                        };
                        if (dr["descricao"] != DBNull.Value)
                            servico.Descricao = dr["descricao"].ToString();

                        servicos.Add(servico);
                    }
                }
                return servicos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os serviços: {ex.Message}");
            }
        }
        public Servico BuscarID(int servicoID)
        {
            try
            {
                string sql = "SELECT * FROM SERVICO WHERE servicoID = @servicoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@servicoID", servicoID);
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Servico servico = new Servico
                            {
                                ServicoID = Convert.ToInt32(dr["servicoID"]),
                                Nome = dr["nome"].ToString() ?? string.Empty,
                                Ativo = Convert.ToBoolean(dr["ativo"])
                            };

                            if (dr["descricao"] != DBNull.Value)
                                servico.Descricao = dr["descricao"].ToString();

                            return servico;
                        }
                        else
                        {
                            throw new Exception("Nenhum serviço encontrado com esse ID.");
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
