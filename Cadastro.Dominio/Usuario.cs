using System.ComponentModel.DataAnnotations;

namespace Cadastro.Dominio
{
    public class Usuario : Pessoa
    {
        public string Rg { get; set; }


        [Required(ErrorMessage = "Informe o senha do usuário")]
        public string Senha { get; set; }
    }
}