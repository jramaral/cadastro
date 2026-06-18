using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Cadastro.Dominio;
using Cadastro.Dominio.Interfaces;

namespace Cadastro.Repositorio.Repositorios
{
    public class DiretorRepositorio : IDiretorRepositorio
    {
        private readonly CadContext _context;

        public DiretorRepositorio()
        {
            _context = new CadContext();
        }

        public Diretor ObterPorId(int id)
        {
            return _context.Diretores.Find(id);
        }


        public IEnumerable<Diretor> ObterTodos()
        {
            return _context.Diretores.AsNoTracking().ToList();
        }


        public void Adicionar(Diretor diretor)
        {
            _context.Diretores.Add(diretor);
            _context.SaveChanges();
        }

        public void Atualizar(Diretor diretor)
        {
            _context.Entry(diretor).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            var diretor = ObterPorId(id);
            _context.Diretores.Remove(diretor);
            _context.SaveChanges();
        }
    }
}