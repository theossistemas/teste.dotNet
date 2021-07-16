using Microsoft.EntityFrameworkCore;
using Livraria.Domain;

namespace Livraria.Infra.Data
{
    public class Context : DbContext
    {

        public DbSet<Livro> Livros { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
