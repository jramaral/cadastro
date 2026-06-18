using System.Collections.Generic;

namespace Cadastro.Dominio.Interfaces
{
    public interface IDiretorRepositorio
    {
        Diretor ObterPorId(int id);
        IEnumerable<Diretor> ObterTodos();
        void Adicionar(Diretor diretor);
        void Atualizar(Diretor diretor);
        void Remover(int id);
    }
}