using GestaoProcessosBDPOO.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using GestaoProcessosBDPOO.Models;
using GestaoProcessosBDPOO.Utilitarios;
using MySql.Data.MySqlClient;

namespace GestaoProcessosBDPOO.Dao
{
    internal class ClienteDAO:IClienteDao
    {
        public void Insert(Cliente cliente)
        {
            try
            {
                string sql = @"INSERT INTO CLIENTE (telefone, email, dataCadastro)
                               VALUES (@telefone, @email, @dataCadastro)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    if (cliente.Telefone != null)
                        cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
                    else
                        cmd.Parameters.AddWithValue("@telefone", DBNull.Value);

                    cmd.Parameters.AddWithValue("@email", cliente.Email);
                    cmd.Parameters.Add("@dataCadastro", MySqlDbType.Date).Value = cliente.DataCadastro.ToDateTime(TimeOnly.MinValue);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Cliente cliente)
        {
            try
            {
                string sql = @"UPDATE CLIENTE SET telefone = @telefone, email = @email, dataCadastro = @dataCadastro
                               WHERE clienteID = @clienteID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    if (cliente.Telefone != null)
                        cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
                    else
                        cmd.Parameters.AddWithValue("@telefone", DBNull.Value);

                    cmd.Parameters.AddWithValue("@email", cliente.Email);
                    cmd.Parameters.Add("@dataCadastro", MySqlDbType.Date).Value = cliente.DataCadastro.ToDateTime(TimeOnly.MinValue);
                    cmd.Parameters.AddWithValue("@clienteID", cliente.ClienteID);
                    var linhas = cmd.ExecuteNonQuery();
                    if (linhas == 0)
                        throw new Exception("Nenhum registro foi atualizado, verifique o clienteID.");
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
                string sql = "DELETE FROM CLIENTE WHERE clienteID = @clienteID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@clienteID", id);
                    var linhasAfetadas = cmd.ExecuteNonQuery();
                    if (linhasAfetadas == 0)
                        throw new Exception("Nenhum cliente registrado encontrado com esse ID.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Cliente> GetAll()
        {
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                string sql = "SELECT * FROM CLIENTE ORDER BY clienteID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Cliente cliente = new Cliente
                        {
                            ClienteID = Convert.ToInt32(dr["clienteID"]),
                            Email = dr["email"].ToString() ?? string.Empty,
                            DataCadastro = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataCadastro"]))
                        };
                        if (dr["telefone"] != DBNull.Value)
                            cliente.Telefone = dr["telefone"].ToString();

                        clientes.Add(cliente);
                    }
                }
                return clientes;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os clientes: {ex.Message}");
            }
        }
        public Cliente BuscarID(int clienteID)
        {
            try
            {
                string sql = "SELECT * FROM CLIENTE WHERE clienteID = @clienteID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@clienteID", clienteID);
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Cliente cliente = new Cliente
                            {
                                ClienteID = Convert.ToInt32(dr["clienteID"]),
                                Email = dr["email"].ToString() ?? string.Empty,
                                DataCadastro = DateOnly.FromDateTime(Convert.ToDateTime(dr["dataCadastro"]))
                            };

                            if (dr["telefone"] != DBNull.Value)
                                cliente.Telefone = dr["telefone"].ToString();

                            return cliente;
                        }
                        else
                        {
                            throw new Exception("Nenhum cliente encontrado com esse ID.");
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
