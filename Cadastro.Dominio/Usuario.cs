using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro.Dominio
{
    public class Usuario:Pessoa
    {
        public string Rg { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Informe o senha do usuário")]
        public String Senha { get; set; }
    }
}
