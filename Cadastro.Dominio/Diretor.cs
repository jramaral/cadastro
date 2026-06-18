using System;

namespace Cadastro.Dominio
{
    public class Diretor
    {
        public int DiretorId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string EnderecoComplemento { get; set; }
        public string Telefone { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Cidade { get; set; }
        public string Escolaridade { get; set; }
        public string Profissao { get; set; }
        public decimal Salario { get; set; }
    }
}