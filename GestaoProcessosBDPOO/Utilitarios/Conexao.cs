using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Utilitarios
{
    internal static class Conexao
    {
        private const string strconexao = "server=localhost;uid=root;pwd=root;database=gestaoProcessos;port=3306";

        public static MySqlConnection Conectar()
        {
            MySqlConnection conexao = new MySqlConnection(strconexao);
            try
            {
                conexao.Open();
                return conexao;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
