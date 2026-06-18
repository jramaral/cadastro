using System.Data.Entity;
using Cadastro.Dominio;

namespace Cadastro.Repositorio
{
    public class CadContext : DbContext
    {
        public CadContext() : base("cadastro")
        {
        }

        public DbSet<Diretor> Diretores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diretor>().ToTable("Diretores");
            base.OnModelCreating(modelBuilder);
        }
    }
}