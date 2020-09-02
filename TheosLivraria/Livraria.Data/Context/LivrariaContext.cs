using Livraria.Data.Mappings;
using Livraria.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Data.Context
{
    public class LivrariaContext : DbContext
    {
        public LivrariaContext(DbContextOptions<LivrariaContext> option) : base(option)
        {
        }

        public DbSet<Livro> Livro { get; set; }
        public DbSet<Autor> Autor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AutorMapping());
            modelBuilder.ApplyConfiguration(new LivroMapping());
        }
    }
}
