using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Cadastro.Repositorio;
using Cadastro.Dominio;

namespace Cadastro.Aplicacao
{
    public class UsuarioAplicacao
    {
        private Contexto contexto;
        

        public void NewUsuario(Usuario usuario)
        {
            var query = string.Format("INSERT Into Usuario(Nome, Cpf, sexo, telefone, DataCadastro, Cidade, Email, Senha) Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", usuario.Nome, usuario.Cpf, usuario.Sexo,usuario.Telefone, usuario.DataCadastro, usuario.Cidade, usuario.Email, usuario.Senha);

            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(query);
            }
        }

        public Usuario ListarPorId(string id, string pw)
        {
            using (contexto = new Contexto())
            {
                var query = string.Format("Select * from Usuario where email like '{0}' and Senha like '{1}' ", id,pw);
                var retorno = contexto.ExecutaComandoRetorno(query);
                return transformaReaderToList(retorno).FirstOrDefault();
            }
        }



        public List<Cidade> EncheComoCidade()
        {
            using (contexto = new Contexto())
            {
                var query = string.Format("Select * from Cidade");
                var retorno = contexto.ExecutaComandoRetorno(query);
                return transformaReaderToCombo(retorno);
            }
        }

        private List<Cidade> transformaReaderToCombo(SqlDataReader reader)
        {
            var cidade = new List<Cidade>();
            while (reader.Read())
            {
                var obj = new Cidade()
                {
                    CodigoCidade = Convert.ToInt32(reader["CodigoCidade"].ToString()),
                    NomeCidade = reader["NomeCidade"].ToString(),
                    UF = reader["UF"].ToString(),
                    Cep = reader["cep"].ToString()

                };
                cidade.Add(obj);
            }
            reader.Close();
            return cidade;

        }

        private List<Usuario> transformaReaderToList(SqlDataReader reader)
        {
            var usuario = new List<Usuario>();
            while(reader.Read())
            {
                var obj = new Usuario()
                {
                    Codigo =Convert.ToInt32(reader["CodigoID"].ToString()),
                    Nome = reader["Nome"].ToString(),
                    Cpf = reader["Cpf"].ToString()

                };
                usuario.Add(obj);
            }
            reader.Close();
            return usuario;

        }
    }
}
