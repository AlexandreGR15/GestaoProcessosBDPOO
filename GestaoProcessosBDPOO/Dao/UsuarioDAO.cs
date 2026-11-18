using System;
using System.Collections.Generic;
using System.Text;
using GestaoProcessosBDPOO.Interfaces;
using GestaoProcessosBDPOO.Models;
using GestaoProcessosBDPOO.Utilitarios;
using MySql.Data.MySqlClient;

namespace GestaoProcessosBDPOO.Dao
{
    internal class UsuarioDAO:IUsuarioDao
    {
        public void Insert(Usuario usuario)
        {
            try
            {
                string sql = @"INSERT INTO USUARIO (nome, email, senha, ativo, cargo)
                           VALUES (@nome, @email, @senha, @ativo, @cargo)";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                    cmd.Parameters.AddWithValue("@email", usuario.Email);
                    cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                    cmd.Parameters.AddWithValue("@ativo", usuario.Ativo);

                    if (usuario.Cargo != null)
                        cmd.Parameters.AddWithValue("@cargo", usuario.Cargo);
                    else
                        cmd.Parameters.AddWithValue("@cargo", DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Usuario usuario)
        {
            try
            {
                string sql = @"UPDATE USUARIO SET nome = @nome, email = @email, senha = @senha, ativo = @ativo, cargo = @cargo
                               WHERE usuarioID = @usuarioID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", usuario.Nome);
                    cmd.Parameters.AddWithValue("@email", usuario.Email);
                    cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                    cmd.Parameters.AddWithValue("@ativo", usuario.Ativo);

                    if (usuario.Cargo != null)
                        cmd.Parameters.AddWithValue("@cargo", usuario.Cargo);
                    else
                        cmd.Parameters.AddWithValue("@cargo", DBNull.Value);

                    cmd.Parameters.AddWithValue("@usuarioID", usuario.UsuarioID);
                    var linhas = cmd.ExecuteNonQuery();
                    if (linhas == 0)
                        throw new Exception("Nenhum registro foi atualizado, verifique o usuarioID.");
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
                string sql = "DELETE FROM USUARIO WHERE usuarioID = @usuarioID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@usuarioID", id);
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
        public List<Usuario> GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                string sql = "SELECT * FROM USUARIO ORDER BY nome";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Usuario usuario = new Usuario
                        {
                            UsuarioID = Convert.ToInt32(dr["usuarioID"]),
                            Nome = dr["nome"].ToString() ?? string.Empty,
                            Email = dr["email"].ToString() ?? string.Empty,
                            Senha = dr["senha"].ToString() ?? string.Empty,
                            Ativo = Convert.ToBoolean(dr["ativo"])
                        };
                        if (dr["cargo"] != DBNull.Value)
                            usuario.Cargo = dr["cargo"].ToString();

                        usuarios.Add(usuario);
                    }
                }
                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os usuários: {ex.Message}");
            }
        }
        public Usuario BuscarID(int usuarioID)
        {
            try
            {
                string sql = "SELECT * FROM USUARIO WHERE usuarioID = @usuarioID";
                using (var conexao = Conexao.Conectar())
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@usuarioID", usuarioID);
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Usuario usuario = new Usuario
                            {
                                UsuarioID = Convert.ToInt32(dr["usuarioID"]),
                                Nome = dr["nome"].ToString() ?? string.Empty,
                                Email = dr["email"].ToString() ?? string.Empty,
                                Senha = dr["senha"].ToString() ?? string.Empty,
                                Ativo = Convert.ToBoolean(dr["ativo"])
                            };

                            if (dr["cargo"] != DBNull.Value)
                                usuario.Cargo = dr["cargo"].ToString();

                            return usuario;
                        }
                        else
                        {
                            throw new Exception("Nenhum usuário encontrado com esse ID.");
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
