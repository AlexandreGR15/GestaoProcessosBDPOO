using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using GestaoProcessosBDPOO.Interfaces;
using GestaoProcessosBDPOO.Models;
using GestaoProcessosBDPOO.Utilitarios;
using MySql.Data.MySqlClient;

namespace GestaoProcessosBDPOO.Dao
{
    internal class ProcessoDAO  : IProcessoDao
    {
        public void Insert(Processo processo)
        {
            try
            {
                string sql = @"INSERT INTO PROCESSO (numProtocolo, dataAbertura, dataPrazo, dataConclusao,
                                status, FK_clienteID, FK_servicoID, FK_usuarioID, FK_imovelID)
                              VALUES (@numProtocolo, @dataAbertura, @dataPrazo, @dataConclusao, 
                                @status, @FK_clienteID, @FK_servicoID, @FK_usuarioID, @FK_imovelID)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@numProtocolo", processo.NumProtocolo);
                    cmd.Parameters.Add("@dataAbertura", MySqlDbType.Date).Value = processo.DataAbertura.ToDateTime(TimeOnly.MinValue);
                    if (processo.DataPrazo.HasValue)
                        cmd.Parameters.Add("@dataPrazo", MySqlDbType.Date).Value = processo.DataPrazo.Value.ToDateTime(TimeOnly.MinValue);
                    else
                        cmd.Parameters.Add("@dataPrazo", MySqlDbType.Date).Value = DBNull.Value;

                    if (processo.DataConclusao.HasValue)
                        cmd.Parameters.Add("@dataConclusao", MySqlDbType.Date).Value = processo.DataConclusao.Value.ToDateTime(TimeOnly.MinValue);
                    else
                        cmd.Parameters.Add("@dataConclusao", MySqlDbType.Date).Value = DBNull.Value;

                    cmd.Parameters.AddWithValue("@status", processo.Status.ToString());
                    cmd.Parameters.AddWithValue("@FK_clienteID", processo.FK_clienteID);
                    cmd.Parameters.AddWithValue("@FK_servicoID", processo.FK_servicoID);
                    cmd.Parameters.AddWithValue("@FK_usuarioID", processo.FK_usuarioID);
                    cmd.Parameters.AddWithValue("@FK_imovelID", processo.FK_imovelID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Processo processo)
        {
            try
            {
                string sql = @"UPDATE PROCESSO SET numProtocolo = @numProtocolo, dataAbertura = @dataAbertura, dataPrazo = @dataPrazo,
                                                   dataConclusao = @dataConclusao, status = @status, FK_clienteID = @FK_clienteID,
                                                   FK_servicoID = @FK_servicoID, FK_usuarioID = @FK_usuarioID, FK_imovelID = @FK_imovelID 
                               WHERE processoID = @processoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@numProtocolo", processo.NumProtocolo);
                    cmd.Parameters.Add("@dataAbertura", MySqlDbType.Date).Value = processo.DataAbertura.ToDateTime(TimeOnly.MinValue);

                    if (processo.DataPrazo.HasValue)
                        cmd.Parameters.Add("@dataPrazo", MySqlDbType.Date).Value = processo.DataPrazo.Value.ToDateTime(TimeOnly.MinValue);
                    else
                        cmd.Parameters.Add("@dataPrazo", MySqlDbType.Date).Value = DBNull.Value;

                    if (processo.DataConclusao.HasValue)
                        cmd.Parameters.Add("@dataConclusao", MySqlDbType.Date).Value = processo.DataConclusao.Value.ToDateTime(TimeOnly.MinValue);
                    else
                        cmd.Parameters.Add("@dataConclusao", MySqlDbType.Date).Value = DBNull.Value;

                    cmd.Parameters.AddWithValue("@status", processo.Status.ToString());
                    cmd.Parameters.AddWithValue("@FK_clienteID", processo.FK_clienteID);
                    cmd.Parameters.AddWithValue("@FK_servicoID", processo.FK_servicoID);
                    cmd.Parameters.AddWithValue("@FK_usuarioID", processo.FK_usuarioID);
                    cmd.Parameters.AddWithValue("@FK_imovelID", processo.FK_imovelID);
                    cmd.Parameters.AddWithValue("@processoID", processo.ProcessoID);
                    var linhas = cmd.ExecuteNonQuery();

                    if (linhas == 0)
                        throw new Exception("Nenhum registro foi atualizado, verifique o processoID.");
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
                string sql = "DELETE FROM PROCESSO WHERE processoID = @processoID";
                using(var conexao = Conexao.Conectar())
                using(var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@processoID", id);
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
        public List<Processo> GetAll()
        {
            List<Processo> processos = new List<Processo>();

            try
            {
                string sql = "SELECT * FROM PROCESSO ORDER BY dataAbertura DESC, processoID DESC";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Processo processo = new Processo
                        {
                            ProcessoID = Convert.ToInt32(dr["processoID"]),
                            NumProtocolo = dr["numProtocolo"].ToString() ?? string.Empty,
                            DataAbertura = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataAbertura"])),
                            Status = Enum.Parse<StatusProcesso>(dr["status"].ToString()!),
                            FK_clienteID = Convert.ToInt32(dr["FK_clienteID"]),
                            FK_servicoID = Convert.ToInt32(dr["FK_servicoID"]),
                            FK_usuarioID = Convert.ToInt32(dr["FK_usuarioID"]),
                            FK_imovelID = Convert.ToInt32(dr["FK_imovelID"])
                        };
                        if (dr["dataPrazo"] != DBNull.Value) 
                            processo.DataPrazo = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataPrazo"]));

                        if (dr["dataConclusao"] != DBNull.Value)
                            processo.DataConclusao = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataConclusao"]));

                        processos.Add(processo);
                    }
                }
                return processos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os processos: {ex.Message}");
            }
        }
        public Processo BuscarID(int processoID)
        {
            try
            {
                string sql = "SELECT * FROM PROCESSO WHERE processoID = @processoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@processoID", processoID);

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Processo processo = new Processo
                            {
                                ProcessoID = Convert.ToInt32(dr["processoID"]),
                                NumProtocolo = dr["numProtocolo"].ToString() ?? string.Empty, 
                                DataAbertura = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataAbertura"])),
                                Status = Enum.Parse<StatusProcesso>(dr["status"].ToString()!),
                                FK_clienteID = Convert.ToInt32(dr["FK_clienteID"]),
                                FK_servicoID = Convert.ToInt32(dr["FK_servicoID"]),
                                FK_usuarioID = Convert.ToInt32(dr["FK_usuarioID"]),
                                FK_imovelID = Convert.ToInt32(dr["FK_imovelID"])
                            };
                            if (dr["dataPrazo"] != DBNull.Value)
                                processo.DataPrazo = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataPrazo"]));

                            if (dr["dataConclusao"] != DBNull.Value)
                                processo.DataConclusao = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataConclusao"]));

                            return processo;
                        }
                        else
                        {
                            throw new Exception("Nenhum processo encontrado com esse ID.");
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
