using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro.Dominio
{
    public class Pessoa
    {
        public int Codigo { get; set; }

        [Required(ErrorMessage = "O nome deve ser informado.!")]
        [StringLength(50, MinimumLength = 5)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "cpf deve ser informado.!")]
        public string Cpf { get; set; }
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O e-mail deve ser informado.!")]
        [StringLength(50, MinimumLength = 5)]
        public string Email { get; set; }

        [Required(ErrorMessage = "O sexo deve ser informado.!")]
        public Genero Sexo { get; set; }

        [Required(ErrorMessage = "A cidade deve ser informado.!")]
        public string Cidade { get; set; }

        [Display(Name = "Data de Cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido]")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido]")]
        public DateTime DataNascimento { get; set; }
    }

    public enum Genero
    {
        M,
        F
    }
}
