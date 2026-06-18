using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Cadastro.Dominio;
using Cadastro.Dominio.Interfaces;
using Cadastro.Repositorio;

namespace Cadastro.Aplicacao
{
    public class DiretorAplicacao
    {
        private readonly IDiretorRepositorio _repositorio;
        
        private Contexto contexto;

        public DiretorAplicacao(IDiretorRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public void CriarDiretor(Diretor diretor)
        {
            _repositorio.Adicionar(diretor);
        }
        
        public void RemoverDiretor(int id)
        {
            using (contexto = new Contexto())
            {
                var query = "DELETE FROM Diretores WHERE DiretorId=" + id;
                contexto.ExecutaComando(query);
            }
        }

        public IEnumerable<Diretor> ObterTodos()
        {
            return _repositorio.ObterTodos();
        }
        public List<Diretor> ListarDiretoresPaginado(int start, int length)
        {
            using (contexto = new Contexto())
            {
                var query = $@"
                    Select *
                    from Diretores
                    Order By DiretorId
                    OFFSET {start} ROWS
                    FETCH NEXT {length} ROWS ONLY";

                var retorno = contexto.ExecutaComandoRetorno(query);
                return transformaReaderToList(retorno).ToList();
            }
        }
        private List<Diretor> transformaReaderToList(SqlDataReader reader)
        {
            var cidade = new List<Diretor>();
            while (reader.Read())
            {
                var obj = new Diretor
                {
                    DiretorId = Convert.ToInt32(reader["DiretorId"].ToString()),
                    Nome = reader["Nome"].ToString(),
                    Email = reader["Email"].ToString(),
                    Telefone = reader["Cidade"].ToString(),
                    Cidade = reader["Telefone"].ToString(),
                    DataCadastro = Convert.ToDateTime(reader["DataCadastro"].ToString())
                };
                cidade.Add(obj);
            }

            reader.Close();
            return cidade;
        }

        public int TotalDiretores()
        {
            var total = 0;
            using (contexto = new Contexto())
            {
                var query = "Select COUNT(*) from Diretores";
                contexto.ExecutaComandoTotalRegistro(query, out total);
                return total;
            }
        }
    }
}