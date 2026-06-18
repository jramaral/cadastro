using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Cadastro.Repositorio
{
    public class Contexto : IDisposable
    {
        private readonly SqlConnection conexao;

        public Contexto()
        {
            conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["cadastro"].ConnectionString);
            conexao.Open();
        }


        public void Dispose()
        {
            if (conexao.State == ConnectionState.Open)
                conexao.Close();
        }

        public void ExecutaComando(string query)
        {
            var cmd = new SqlCommand
            {
                CommandText = query,
                CommandType = CommandType.Text,
                Connection = conexao
            };
            cmd.ExecuteNonQuery();
        }

        public void ExecutaComando(string query, out int i)
        {
            var cmd = new SqlCommand
            {
                CommandText = query,
                CommandType = CommandType.Text,
                Connection = conexao
            };
            cmd.ExecuteNonQuery();
            cmd.CommandText = "Select @@IDENTITY";
            i = Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void ExecutaComandoTotalRegistro(string query, out int i)
        {
            var cmd = new SqlCommand
            {
                CommandText = query,
                CommandType = CommandType.Text,
                Connection = conexao
            };
            i = Convert.ToInt32(cmd.ExecuteScalar());
        }


        public SqlDataReader ExecutaComandoRetorno(string query)
        {
            var cmd = new SqlCommand(query, conexao);
            return cmd.ExecuteReader();
        }
    }
}