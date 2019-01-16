using livraria_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace livraria_backend.Data
{
    public class LivrariaContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public LivrariaContext(DbContextOptions<LivrariaContext> options):base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {    
            modelBuilder.Entity<Livro>();
            modelBuilder.Entity<Usuario>();
        }
    }
}