using GestaoProcessosBDPOO.Interfaces;
using GestaoProcessosBDPOO.Models;
using GestaoProcessosBDPOO.Utilitarios;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Dao
{
    internal class DocumentoDAO:IDocumentoDao
    {
        public void Insert(Documento documento)
        {
            try
            {
                string sql = @"INSERT INTO DOCUMENTO (nomeArquivo, tipoDocumento, dataEmissao, dataValidade, dataRecebimento, situacao, FK_processoID)
                               VALUES (@nomeArquivo, @tipoDocumento, @dataEmissao, @dataValidade, @dataRecebimento, @situacao, @FK_processoID)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nomeArquivo", documento.NomeArquivo);
                    cmd.Parameters.AddWithValue("@tipoDocumento", documento.TipoDocumento);
                    cmd.Parameters.Add("@dataEmissao", MySqlDbType.Date).Value = documento.DataEmissao.ToDateTime(TimeOnly.MinValue);
                    if (documento.DataValidade.HasValue)
                        cmd.Parameters.Add("@dataValidade", MySqlDbType.Date).Value = documento.DataValidade.Value.ToDateTime(TimeOnly.MinValue);
                    else
                        cmd.Parameters.Add("@dataValidade", MySqlDbType.Date).Value = DBNull.Value;

                    if (documento.DataRecebimento.HasValue)
                        cmd.Parameters.Add("@dataRecebimento", MySqlDbType.Date).Value = documento.DataRecebimento.Value.ToDateTime(TimeOnly.MinValue);
                    else
                        cmd.Parameters.Add("@dataRecebimento", MySqlDbType.Date).Value = DBNull.Value;

                    cmd.Parameters.AddWithValue("@situacao", documento.Situacao);
                    cmd.Parameters.AddWithValue("@processoID", documento.FK_processoID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Documento documento)
        {
            try
            {
                string sql = @"UPDATE DOCUMENTO SET nomeArquivo = @nomeArquivo, tipoDocumento = @tipoDocumento, dataEmissao = @dataEmissao, dataValidade = @dataValidade, dataRecebimento = @dataRecebimento, situacao = @situacao, FK_processoID = @FK_processoID
                               WHERE documentoID = @documentoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nomeArquivo", documento.NomeArquivo);
                    cmd.Parameters.AddWithValue("@tipoDocumento", documento.TipoDocumento);
                    cmd.Parameters.Add("@dataEmissao", MySqlDbType.Date).Value = documento.DataEmissao.ToDateTime(TimeOnly.MinValue);
                    if (documento.DataValidade.HasValue)
                        cmd.Parameters.Add("@dataValidade", MySqlDbType.Date).Value = documento.DataValidade.Value.ToDateTime(TimeOnly.MinValue);
                    else
                        cmd.Parameters.Add("@dataValidade", MySqlDbType.Date).Value = DBNull.Value;

                    if (documento.DataRecebimento.HasValue)
                        cmd.Parameters.Add("@dataRecebimento", MySqlDbType.Date).Value = documento.DataRecebimento.Value.ToDateTime(TimeOnly.MinValue);
                    else
                        cmd.Parameters.Add("@dataRecebimento", MySqlDbType.Date).Value = DBNull.Value;

                    cmd.Parameters.AddWithValue("@situacao", documento.Situacao);
                    cmd.Parameters.AddWithValue("@processoID", documento.FK_processoID);
                    cmd.Parameters.AddWithValue("@documentoID", documento.DocumentoID);

                    var linhas = cmd.ExecuteNonQuery();
                    if (linhas == 0)
                        throw new Exception("Nenhum registro foi atualizado, verifique o documentoID.");
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
                string sql = "DELETE FROM DOCUMENTO WHERE documentoID = @documentoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@documentoID", id);
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
        public List<Documento> GetAll()
        {
            List<Documento> documentos = new List<Documento>();
            try
            {
                string sql = "SELECT * FROM DOCUMENTO ORDER BY FK_processoID, documentoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Documento documento = new Documento
                        {
                            DocumentoID = Convert.ToInt32(dr["documentoID"]),
                            NomeArquivo = dr["nomeArquivo"].ToString() ?? string.Empty,
                            TipoDocumento = dr["tipoDocumento"].ToString() ?? string.Empty,
                            DataEmissao = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataEmissao"])),
                            Situacao = dr["situacao"].ToString() ?? string.Empty,
                            FK_processoID = Convert.ToInt32(dr["FK_processoID"])
                        };
                        if (dr["dataValidade"] != DBNull.Value)
                            documento.DataValidade = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataValidade"]));

                        if (dr["dataRecebimento"] != DBNull.Value)
                            documento.DataRecebimento = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataRecebimento"]));

                        documentos.Add(documento);
                    }
                }
                return documentos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os documentos: {ex.Message}");
            }
        }
        public Documento BuscarID(int documentoID)
        {
            try
            {
                string sql = "SELECT * FROM DOCUMENTO WHERE documentoID = @documentoID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@documentoID", documentoID);
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Documento documento = new Documento
                            {
                                DocumentoID = Convert.ToInt32(dr["documentoID"]),
                                NomeArquivo = dr["nomeArquivo"].ToString() ?? string.Empty,
                                TipoDocumento = dr["tipoDocumento"].ToString() ?? string.Empty,
                                DataEmissao = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataEmissao"])),
                                Situacao = dr["situacao"].ToString() ?? string.Empty,
                                FK_processoID = Convert.ToInt32(dr["FK_processoID"])
                            };
                            if (dr["dataValidade"] != DBNull.Value)
                                documento.DataValidade = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataValidade"]));

                            if (dr["dataRecebimento"] != DBNull.Value)
                                documento.DataRecebimento = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataRecebimento"]));

                            return documento;
                        }
                        else
                        {
                            throw new Exception("Nenhum documento encontrado com esse ID.");
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
