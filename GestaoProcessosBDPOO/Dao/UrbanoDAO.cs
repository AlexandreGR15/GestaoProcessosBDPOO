using GestaoProcessosBDPOO.Interfaces;
using GestaoProcessosBDPOO.Models;
using GestaoProcessosBDPOO.Utilitarios;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Dao
{
    internal class UrbanoDAO:IUrbanoDao
    {
        public void Insert(Urbano urbano)
        {
            try
            {
                string sql = @"INSERT INTO URBANO (FK_imovelID, setor, quadra, lote, areaMtQuadrado)
                               VALUES (@FK_imovelID, @setor, @quadra, @lote, @areaMtQuadrado)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@FK_imovelID", urbano.FK_imovelID);
                    cmd.Parameters.AddWithValue("@setor", urbano.Setor);
                    cmd.Parameters.AddWithValue("@quadra", urbano.Quadra);
                    cmd.Parameters.AddWithValue("@lote", urbano.Lote);
                    if (urbano.AreaMtQuadrado.HasValue)
                        cmd.Parameters.AddWithValue("@areaMtQuadrado", urbano.AreaMtQuadrado.Value);
                    else
                        cmd.Parameters.AddWithValue("@areaMtQuadrado", DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Urbano urbano)
        {
            try
            {
                string sql = @"UPDATE URBANO SET setor = @setor, quadra = @quadra, lote = @lote, areaMtQuadrado = @areaMtQuadrado
                               WHERE FK_imovelID = @FK_imovelID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@setor", urbano.Setor);
                    cmd.Parameters.AddWithValue("@quadra", urbano.Quadra);
                    cmd.Parameters.AddWithValue("@lote", urbano.Lote);
                    if (urbano.AreaMtQuadrado.HasValue)
                        cmd.Parameters.AddWithValue("@areaMtQuadrado", urbano.AreaMtQuadrado.Value);
                    else
                        cmd.Parameters.AddWithValue("@areaMtQuadrado", DBNull.Value);

                    cmd.Parameters.AddWithValue("@FK_imovelID", urbano.FK_imovelID);

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
                string sql = "DELETE FROM URBANO WHERE FK_imovelID = @FK_imovelID";
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
        public List<Urbano> GetAll()
        {
            List<Urbano> imoveisUrbano = new List<Urbano>();
            try
            {
                string sql = "SELECT * FROM URBANO ORDER BY FK_imovelID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Urbano urbano = new Urbano
                        {
                            FK_imovelID = Convert.ToInt32(dr["FK_imovelID"]),
                            Setor = dr["setor"].ToString() ?? string.Empty,
                            Quadra = dr["quadra"].ToString() ?? string.Empty,
                            Lote = dr["lote"].ToString() ?? string.Empty
                        };
                        if (dr["areaMtQuadrado"] != DBNull.Value)
                            urbano.AreaMtQuadrado = Convert.ToDecimal(dr["areaMtQuadrado"]);

                        imoveisUrbano.Add(urbano);
                    }
                }
                return imoveisUrbano;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os imóveis urbanos: {ex.Message}");
            }
        }
        public Urbano BuscarID(int FK_imovelID)
        {
            try
            {
                string sql = "SELECT * FROM URBANO WHERE FK_imovelID = @FK_imovelID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@FK_imovelID", FK_imovelID);
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Urbano urbano = new Urbano
                            {
                                FK_imovelID = Convert.ToInt32(dr["FK_imovelID"]),
                                Setor = dr["setor"].ToString() ?? string.Empty,
                                Quadra = dr["quadra"].ToString() ?? string.Empty,
                                Lote = dr["lote"].ToString() ?? string.Empty
                            };
                            if (dr["areaMtQuadrado"] != DBNull.Value)
                                urbano.AreaMtQuadrado = Convert.ToDecimal(dr["areaMtQuadrado"]);

                            return urbano;
                        }
                        else
                        {
                            throw new Exception("Nenhum imóvel urbano encontrado com esse ID.");
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
