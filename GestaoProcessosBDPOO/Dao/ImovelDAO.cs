using GestaoProcessosBDPOO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using GestaoProcessosBDPOO.Models;
using GestaoProcessosBDPOO.Utilitarios;
using MySql.Data.MySqlClient;

namespace GestaoProcessosBDPOO.Dao
{
    internal class ImovelDAO:IImovelDao
    {
        public void Insert(Imovel imovel)
        {
            try
            {
                string sql = @"INSERT INTO IMOVEL (denominacao, municipio, uf, endereco, matricula)
                               VALUES (@denominacao, @municipio, @uf, @endereco, @matricula)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@denominacao", imovel.Denominacao);
                    cmd.Parameters.AddWithValue("@municipio", imovel.Municipio);
                    cmd.Parameters.AddWithValue("@uf", imovel.Uf);
                    if (imovel.Endereco != null)
                        cmd.Parameters.AddWithValue("@endereco", imovel.Endereco);
                    else
                        cmd.Parameters.AddWithValue("@endereco", DBNull.Value);

                    cmd.Parameters.AddWithValue("@matricula", imovel.Matricula);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Imovel imovel)
        {
            try
            {
                string sql = @"UPDATE IMOVEL SET denominacao = @denominacao, municipio = @municipio, uf = @uf, endereco = @endereco, matricula = @matricula
                               WHERE imovelID = @imovelID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@denominacao", imovel.Denominacao);
                    cmd.Parameters.AddWithValue("@municipio", imovel.Municipio);
                    cmd.Parameters.AddWithValue("@uf", imovel.Uf);
                    if (imovel.Endereco != null)
                        cmd.Parameters.AddWithValue("@endereco", imovel.Endereco);
                    else
                        cmd.Parameters.AddWithValue("@endereco", DBNull.Value);

                    cmd.Parameters.AddWithValue("@matricula", imovel.Matricula);
                    cmd.Parameters.AddWithValue("@imovelID", imovel.ImovelID);
                    var linhas = cmd.ExecuteNonQuery();
                    if (linhas == 0)
                        throw new Exception("Nenhum registro foi atualizado, verifique o imovelID.");
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
                string sql = "DELETE FROM IMOVEL WHERE imovelID = @imovelID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@imovelID", id);
                    var linhasAfetadas = cmd.ExecuteNonQuery();
                    if (linhasAfetadas == 0)
                        throw new Exception("Nenhum imóvel registro encontrado com esse ID.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Imovel> GetAll()
        {
            List<Imovel> imoveis = new List<Imovel>();
            try
            {
                string sql = "SELECT * FROM IMOVEL ORDER BY municipio, denominacao";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Imovel imovel = new Imovel
                        {
                            ImovelID = Convert.ToInt32(dr["imovelID"]),
                            Denominacao = dr["denominacao"].ToString() ?? string.Empty,
                            Municipio = dr["municipio"].ToString() ?? string.Empty,
                            Uf = dr["uf"].ToString() ?? string.Empty,
                            Matricula = dr["matricula"].ToString() ?? string.Empty
                        };
                        if (dr["endereco"] != DBNull.Value)
                            imovel.Endereco = dr["endereco"].ToString();

                        imoveis.Add(imovel);
                    }
                }
                return imoveis;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os imóveis: {ex.Message}");
            }
        }
        public Imovel BuscarID(int imovelID)
        {
            try
            {
                string sql = "SELECT * FROM IMOVEL WHERE imovelID = @imovelID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@imovelID", imovelID);
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Imovel imovel = new Imovel
                            {
                                ImovelID = Convert.ToInt32(dr["imovelID"]),
                                Denominacao = dr["denominacao"].ToString() ?? string.Empty,
                                Municipio = dr["municipio"].ToString() ?? string.Empty,
                                Uf = dr["uf"].ToString() ?? string.Empty,
                                Matricula = dr["matricula"].ToString() ?? string.Empty
                            };
                            if (dr["endereco"] != DBNull.Value)
                                imovel.Endereco = dr["endereco"].ToString();

                            return imovel;
                        }
                        else
                        {
                            throw new Exception("Nenhum imóvel encontrado com esse ID.");
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
