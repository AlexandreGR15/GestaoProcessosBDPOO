using GestaoProcessosBDPOO.Interfaces;
using GestaoProcessosBDPOO.Models;
using GestaoProcessosBDPOO.Utilitarios;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Dao
{
    internal class PessoaFisicaDAO:IPessoaFisicaDao
    {
        
        public void Insert(PessoaFisica pessoaFisica)
        {
            try
            {
                string sql = @"INSERT INTO PESSOA_FISICA (FK_clienteID, cpf, nome, dataNasc)
                               VALUES (@FK_clienteID, @cpf, @nome, @dataNasc)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@FK_clienteID", pessoaFisica.FK_clienteID);
                    cmd.Parameters.AddWithValue("@cpf", pessoaFisica.Cpf);
                    cmd.Parameters.AddWithValue("@nome", pessoaFisica.Nome);
                    if (pessoaFisica.DataNasc.HasValue)
                        cmd.Parameters.Add("@dataNasc", MySqlDbType.Date).Value = pessoaFisica.DataNasc.Value.ToDateTime(TimeOnly.MinValue);
                    else
                        cmd.Parameters.Add("@dataNasc", MySqlDbType.Date).Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(PessoaFisica pessoaFisica)
        {
            try
            {
                string sql = @"UPDATE PESSOA_FISICA SET cpf = @cpf, nome = @nome, dataNasc = @dataNasc
                               WHERE FK_clienteID = @FK_clienteID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@cpf", pessoaFisica.Cpf);
                    cmd.Parameters.AddWithValue("@nome", pessoaFisica.Nome);
                    if (pessoaFisica.DataNasc.HasValue)
                        cmd.Parameters.Add("@dataNasc", MySqlDbType.Date).Value = pessoaFisica.DataNasc.Value.ToDateTime(TimeOnly.MinValue);
                    else
                        cmd.Parameters.Add("@dataNasc", MySqlDbType.Date).Value = DBNull.Value;

                    cmd.Parameters.AddWithValue("@FK_clienteID", pessoaFisica.FK_clienteID);
                    var linhas = cmd.ExecuteNonQuery();
                    if (linhas == 0)
                        throw new Exception("Nenhum registro foi atualizado, verifique o FK_clienteID.");
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
                string sql = "DELETE FROM PESSOA_FISICA WHERE FK_clienteID = @FK_clienteID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@FK_clienteID", id);
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
        public List<PessoaFisica> GetAll()
        {
            List<PessoaFisica> clientesFisico = new List<PessoaFisica>();
            try
            {
                string sql = "SELECT * FROM PESSOA_FISICA ORDER BY nome";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        PessoaFisica pessoaFisica = new PessoaFisica
                        {
                            FK_clienteID = Convert.ToInt32(dr["FK_clienteID"]),
                            Cpf = dr["cpf"].ToString() ?? string.Empty,
                            Nome = dr["nome"].ToString() ?? string.Empty
                        };
                        if (dr["dataNasc"] != DBNull.Value)
                            pessoaFisica.DataNasc = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataNasc"]));

                        clientesFisico.Add(pessoaFisica);
                    }
                }
                return clientesFisico;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os pessoas físicas: {ex.Message}");
            }
        }
        public PessoaFisica BuscarID(int FK_clienteID)
        {
            try
            {
                string sql = "SELECT * FROM PESSOA_FISICA WHERE FK_clienteID = @FK_clienteID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@FK_clienteID", FK_clienteID);
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            PessoaFisica pessoaFisica = new PessoaFisica
                            {
                                FK_clienteID = Convert.ToInt32(dr["FK_clienteID"]),
                                Cpf = dr["cpf"].ToString() ?? string.Empty,
                                Nome = dr["nome"].ToString() ?? string.Empty
                            };
                            if (dr["dataNasc"] != DBNull.Value)
                                pessoaFisica.DataNasc = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataNasc"]));

                            return pessoaFisica;
                        }
                        else
                        {
                            throw new Exception("Nenhum pessoa física encontrado com esse ID.");
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
