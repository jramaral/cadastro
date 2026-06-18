using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Cadastro.Dominio;
using Cadastro.Repositorio;

namespace Cadastro.Aplicacao
{
    public class CidadeAplicacao
    {
        private Contexto contexto;

        public List<Cidade> ListarCidade()
        {
            using (contexto = new Contexto())
            {
                var query = "Select * from Cidade";
                var retorno = contexto.ExecutaComandoRetorno(query);
                return transformaReaderToList(retorno);
            }
        }

        public void Inserir(Cidade cidade)
        {
            var query = string.Format("INSERT INTO Cidade (NomeCidade, Uf, Cep) Values('{0}','{1}','{2}')",
                cidade.NomeCidade, cidade.UF, cidade.Cep);
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(query);
            }
        }

        public void AtualizarCidade(Cidade cidade)
        {
            var query = string.Format("UPDATE CIDADE SET NomeCidade='{0}',UF='{1}',Cep='{2}' WHERE CodigoCidade = {3}",
                cidade.NomeCidade, cidade.UF, cidade.Cep, cidade.CodigoCidade);

            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(query);
            }
        }

        public Cidade ListarPorId(string id)
        {
            using (contexto = new Contexto())
            {
                var query = "Select * from Cidade Where CodigoCidade=" + int.Parse(id);
                var retorno = contexto.ExecutaComandoRetorno(query);
                return transformaReaderToList(retorno).FirstOrDefault();
            }
        }

        public void RemoverCidade(int id)
        {
            using (contexto = new Contexto())
            {
                var query = "DELETE FROM Cidade WHERE CodigoCidade=" + id;
                contexto.ExecutaComando(query);
            }
        }

        public List<Cidade> ListarCidadePaginado(int start, int length)
        {
            using (contexto = new Contexto())
            {
                var query = $@"
                    Select *
                    from Cidade
                    Order By CodigoCidade
                    OFFSET {start} ROWS
                    FETCH NEXT {length} ROWS ONLY";

                var retorno = contexto.ExecutaComandoRetorno(query);
                return transformaReaderToList(retorno).ToList();
            }
        }

        private List<Cidade> transformaReaderToList(SqlDataReader reader)
        {
            var cidade = new List<Cidade>();
            while (reader.Read())
            {
                var obj = new Cidade
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

        public int TotalCidades()
        {
            var total = 0;
            using (contexto = new Contexto())
            {
                var query = "Select COUNT(*) from Cidade";
                contexto.ExecutaComandoTotalRegistro(query, out total);
                return total;
            }
        }
    }
}