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
    public class AlunoAplicacao
    {
        private Contexto contexto;


        public void NewAluno(Aluno aluno)
        {
            int codAl=0;
            var query = string.Format("INSERT Into Alunos (Nome, Cpf, rg , DataNascimento, EnderecoComplemento, Matricula, Idade, Sexo, Telefone, DataCadastro, Cidade, Email) Values('{0}','{1}','{2}', CONVERT(DATE,'{3}',103),'{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')"
                , aluno.Nome, aluno.Cpf, aluno.Rg, aluno.DataNascimento, aluno.Endereco, aluno.Matricula, aluno.Idade, aluno.Sexo, aluno.Telefone, aluno.DataCadastro, aluno.Cidade, aluno.Email);

            var query1 = string.Format("INSERT INTO Filiacao Values({0},'{1}','{2}','{3}','{4}')", codAl, aluno.NomePai, aluno.NomeMae,string.Empty,string.Empty);
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(query, out codAl);
                contexto.ExecutaComando(string.Format("INSERT INTO Filiacao Values({0},'{1}','{2}','{3}','{4}')", codAl, aluno.NomePai, aluno.NomeMae, string.Empty, string.Empty));
            }
        }

        public void AtualizarAluno(Aluno aluno)
        {
            var query = string.Format("UPDATE Alunos SET Nome='{0}', Cpf='{1}', rg='{2}' , DataNascimento=CONVERT(DATE,'{3}'103), EnderecoComplemento='{4}', Matricula='{5}', Idade='{6}', Sexo='{7}', Telefone='{8}', DataCadastro='{9}', Cidade='{10}', Email='{11}' WHERE CodigoID={12}"
               , aluno.Nome, aluno.Cpf, aluno.Rg, aluno.DataNascimento, aluno.Endereco, aluno.Matricula, aluno.Idade, aluno.Sexo, aluno.Telefone, aluno.DataCadastro, aluno.Cidade, aluno.Email, aluno.Codigo);

            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(query);
            }
        }

        public List<Aluno> ListarAluno()
        {
            using (contexto = new Contexto())
            {
                var query = string.Format("Select * from Alunos left join Filiacao on (Alunos.CodigoID=Filiacao.CodigoID_Aluno)");
                var retorno = contexto.ExecutaComandoRetorno(query);
                return transformaReaderToList(retorno);
            }
        }

        public Aluno ListarPorId(string id)
        {
            using (contexto = new Contexto())
            {

                var query = string.Format("Select * from Alunos Left join Filiacao on (Alunos.CodigoID = Filiacao.CodigoID_Aluno) where CodigoID={0}", id);
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

        private List<Aluno> transformaReaderToList(SqlDataReader reader)
        {
            var aluno = new List<Aluno>();

            
            while (reader.Read())
            {
                var obj = new Aluno()
                {
                    
                    Codigo = Convert.ToInt32(reader["CodigoID"].ToString()),
                    Nome = reader["Nome"].ToString(),
                    Cpf = reader["Cpf"].ToString(),
                    Rg = reader["rg"].ToString(),
                    DataNascimento =Convert.ToDateTime(reader["DataNascimento"].ToString()),
                    Endereco = reader["EnderecoComplemento"].ToString(),
                    Matricula = reader["Matricula"].ToString(),
                    Idade =Convert.ToInt32(reader["idade"].ToString()),
                    Sexo = (Genero)Enum.Parse(typeof(Genero),reader["Sexo"].ToString()), 
                    Telefone = reader["telefone"].ToString(),
                    Cidade=reader["cidade"].ToString(),
                    DataCadastro=Convert.ToDateTime(reader["DataCadastro"].ToString()),
                    Email=reader["email"].ToString(),
                    NomePai=reader["NomePai"].ToString(),
                    NomeMae=reader["NomeMae"].ToString()
                    
                    


                };
                aluno.Add(obj);
            }
            reader.Close();
            return aluno;

        }

    }
}
