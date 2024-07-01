using LivrariaJc.Data.EntityBuilders;
using LivrariaJc.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LivrariaJc.Data.Context
{
    public class LivrariaJcContext : DbContext
    {
        public LivrariaJcContext(DbContextOptions<LivrariaJcContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<LivrosEntidade>(new LivroMapping().Configure);
        }

        public DbSet<LivrosEntidade> Livro { get; set; }
    }
}
