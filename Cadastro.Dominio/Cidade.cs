using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro.Dominio
{
    public class Cidade
    {
        public int CodigoCidade { get; set; }
        public string NomeCidade { get; set; }
        public string UF { get; set; }
        public string Cep { get; set; }
    }
}
