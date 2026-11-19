using GestaoProcessosBDPOO.Interfaces;
using GestaoProcessosBDPOO.Models;
using GestaoProcessosBDPOO.Utilitarios;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Dao
{
    internal class PessoaJuridicaDAO:IPessoaJuridicaDao
    {
        public void Insert(PessoaJuridica pessoaJuridica)
        {
            try
            {
                string sql = @"INSERT INTO PESSOA_JURIDICA (FK_clienteID, cnpj, razaoSocial)
                               VALUES (@FK_clienteID, @cnpj, @razaoSocial)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@FK_clienteID", pessoaJuridica.FK_clienteID);
                    cmd.Parameters.AddWithValue("@cnpj", pessoaJuridica.Cnpj);
                    cmd.Parameters.AddWithValue("@razaoSocial", pessoaJuridica.RazaoSocial);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(PessoaJuridica pessoaJuridica)
        {
            try
            {
                string sql = @"UPDATE PESSOA_JURIDICA SET cnpj = @cnpj, razaoSocial = @razaoSocial
                               WHERE FK_clienteID = @FK_clienteID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@cnpj", pessoaJuridica.Cnpj);
                    cmd.Parameters.AddWithValue("@razaoSocial", pessoaJuridica.RazaoSocial);
                    cmd.Parameters.AddWithValue("@FK_clienteID", pessoaJuridica.FK_clienteID);

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
                string sql = "DELETE FROM PESSOA_JURIDICA WHERE FK_clienteID = @FK_clienteID";
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
        public List<PessoaJuridica> GetAll()
        {
            List<PessoaJuridica> clientesJuridico = new List<PessoaJuridica>();
            try
            {
                string sql = "SELECT * FROM PESSOA_JURIDICA ORDER BY razaoSocial";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        PessoaJuridica pessoaJuridica = new PessoaJuridica
                        {
                            FK_clienteID = Convert.ToInt32(dr["FK_clienteID"]),
                            Cnpj = dr["cnpj"].ToString() ?? string.Empty,
                            RazaoSocial = dr["razaoSocial"].ToString() ?? string.Empty
                        };

                        clientesJuridico.Add(pessoaJuridica);
                    }
                }
                return clientesJuridico;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os pessoas jurídicas: {ex.Message}");
            }
        }
        public PessoaJuridica BuscarID(int FK_clienteID)
        {
            try
            {
                string sql = "SELECT * FROM PESSOA_JURIDICA WHERE FK_clienteID = @FK_clienteID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@FK_clienteID", FK_clienteID);
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            PessoaJuridica pessoaJuridica = new PessoaJuridica
                            {
                                FK_clienteID = Convert.ToInt32(dr["FK_clienteID"]),
                                Cnpj = dr["cnpj"].ToString() ?? string.Empty,
                                RazaoSocial = dr["razaoSocial"].ToString() ?? string.Empty
                            };
                            return pessoaJuridica;
                        }
                        else
                        {
                            throw new Exception("Nenhum pessoa jurídica encontrado com esse ID.");
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
