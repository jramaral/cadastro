using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro.Dominio
{
    public class Aluno : Pessoa
    {
        public string Endereco { get; set; }
        public string ProfissaoMae { get; set; }
        public string ProfissaoPai { get; set; }
        public string NomeMae { get; set; }
        public string NomePai { get; set; }
        public string Matricula { get; set; }
        public string Rg { get; set; }
        public int Idade { get; set; }
    }
}
