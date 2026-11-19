using GestaoProcessosBDPOO.Interfaces;
using GestaoProcessosBDPOO.Models;
using GestaoProcessosBDPOO.Utilitarios;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Dao
{
    internal class RuralDAO:IRuralDao
    {
        public void Insert(Rural rural)
        {
            try
            {
                string sql = @"INSERT INTO RURAL (FK_imovelID, codigoIncra, areaHa)
                               VALUES (@FK_imovelID, @codigoIncra, @areaHa)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@FK_imovelID", rural.FK_imovelID);
                    cmd.Parameters.AddWithValue("@codigoIncra", rural.CodigoIncra);
                    if (rural.AreaHa.HasValue)
                        cmd.Parameters.AddWithValue("@areaHa", rural.AreaHa.Value);
                    else
                        cmd.Parameters.AddWithValue("@areaHa", DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Rural rural)
        {
            try
            {
                string sql = @"UPDATE RURAL SET codigoIncra = @codigoIncra, areaHa = @areaHa
                               WHERE FK_imovelID = @FK_imovelID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@codigoIncra", rural.CodigoIncra);
                    if (rural.AreaHa.HasValue)
                        cmd.Parameters.AddWithValue("@areaHa", rural.AreaHa.Value);
                    else
                        cmd.Parameters.AddWithValue("@areaHa", DBNull.Value);

                    cmd.Parameters.AddWithValue("@FK_imovelID", rural.FK_imovelID);
                    var linhas = cmd.ExecuteNonQuery();
                    if (linhas == 0)
                        throw new Exception("Nenhum registro foi atualizado, verifique o FK_imovelID.");
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
                string sql = "DELETE FROM RURAL WHERE FK_imovelID = @FK_imovelID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@FK_imovelID", id);
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
        public List<Rural> GetAll()
        {
            List<Rural> imoveisRural = new List<Rural>();
            try
            {
                string sql = "SELECT * FROM RURAL ORDER BY FK_imovelID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Rural rural = new Rural
                        {
                            FK_imovelID = Convert.ToInt32(dr["FK_imovelID"]),
                            CodigoIncra = dr["codigoIncra"].ToString() ?? string.Empty
                        };
                        if (dr["areaHa"] != DBNull.Value)
                            rural.AreaHa = Convert.ToDecimal(dr["areaHa"]);

                        imoveisRural.Add(rural);
                    }
                }

                return imoveisRural;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os imóveis rurais: {ex.Message}");
            }
        }
        public Rural BuscarID(int FK_imovelID)
        {
            try
            {
                string sql = "SELECT * FROM RURAL WHERE FK_imovelID = @FK_imovelID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@FK_imovelID", FK_imovelID);
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Rural rural = new Rural
                            {
                                FK_imovelID = Convert.ToInt32(dr["FK_imovelID"]),
                                CodigoIncra = dr["codigoIncra"].ToString() ?? string.Empty
                            };
                            if (dr["areaHa"] != DBNull.Value)
                                rural.AreaHa = Convert.ToDecimal(dr["areaHa"]);

                            return rural;
                        }
                        else
                        {
                            throw new Exception("Nenhum imóvel rural encontrado com esse ID.");
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
