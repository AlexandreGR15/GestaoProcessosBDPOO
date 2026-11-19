using GestaoProcessosBDPOO.Interfaces;
using GestaoProcessosBDPOO.Models;
using GestaoProcessosBDPOO.Utilitarios;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Dao
{
    internal class ClienteImovelDAO:IClienteImovelDao
    {
        public void Insert(ClienteImovel vinculo)
        {
            try
            {
                string sql = @"INSERT INTO CLIENTE_IMOVEL (percentual, tipoVinculo, FK_clienteID, FK_imovelID)
                               VALUES (@percentual, @tipoVinculo, @FK_clienteID, @FK_imovelID)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@percentual", vinculo.Percentual);
                    cmd.Parameters.AddWithValue("@tipoVinculo", vinculo.TipoVinculo);
                    cmd.Parameters.AddWithValue("@FK_clienteID", vinculo.FK_clienteID);
                    cmd.Parameters.AddWithValue("@FK_imovelID", vinculo.FK_imovelID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(ClienteImovel vinculo)
        {
            try
            {
                string sql = @"UPDATE CLIENTE_IMOVEL SET percentual = @percentual, tipoVinculo = @tipoVinculo, FK_clienteID = @FK_clienteID, FK_imovelID = @FK_imovelID
                               WHERE vinculoID = @vinculoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@percentual", vinculo.Percentual);
                    cmd.Parameters.AddWithValue("@tipoVinculo", vinculo.TipoVinculo);
                    cmd.Parameters.AddWithValue("@FK_clienteID", vinculo.FK_clienteID);
                    cmd.Parameters.AddWithValue("@FK_imovelID", vinculo.FK_imovelID);
                    cmd.Parameters.AddWithValue("@vinculoID", vinculo.VinculoID);
                    var linhas = cmd.ExecuteNonQuery();
                    if (linhas == 0)
                        throw new Exception("Nenhum registro foi atualizado, verifique o vinculoID.");
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
                string sql = "DELETE FROM CLIENTE_IMOVEL WHERE vinculoID = @vinculoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@vinculoID", id);
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
        public List<ClienteImovel> GetAll()
        {
            List<ClienteImovel> vinculos = new List<ClienteImovel>();
            try
            {
                string sql = "SELECT * FROM CLIENTE_IMOVEL ORDER BY FK_imovelID, FK_clienteID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ClienteImovel vinculo = new ClienteImovel
                        {
                            VinculoID = Convert.ToInt32(dr["vinculoID"]),
                            Percentual = Convert.ToDecimal(dr["percentual"]),
                            TipoVinculo = dr["tipoVinculo"].ToString() ?? string.Empty,
                            FK_clienteID = Convert.ToInt32(dr["FK_clienteID"]),
                            FK_imovelID = Convert.ToInt32(dr["FK_imovelID"])
                        };
                        vinculos.Add(vinculo);
                    }
                }
                return vinculos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os vínculos: {ex.Message}");
            }
        }
        public ClienteImovel BuscarID(int vinculoID)
        {
            try
            {
                string sql = "SELECT * FROM CLIENTE_IMOVEL WHERE vinculoID = @vinculoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@vinculoID", vinculoID);
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            ClienteImovel vinculo = new ClienteImovel
                            {
                                VinculoID = Convert.ToInt32(dr["vinculoID"]),
                                Percentual = Convert.ToDecimal(dr["percentual"]),
                                TipoVinculo = dr["tipoVinculo"].ToString() ?? string.Empty,
                                FK_clienteID = Convert.ToInt32(dr["FK_clienteID"]),
                                FK_imovelID = Convert.ToInt32(dr["FK_imovelID"])
                            };
                            return vinculo;
                        }
                        else
                        {
                            throw new Exception("Nenhum vínculo encontrado com esse ID.");
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
