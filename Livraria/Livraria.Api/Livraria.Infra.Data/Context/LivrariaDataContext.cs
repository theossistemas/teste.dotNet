using Livraria.Domain.Entities.Administracao;
using Livraria.Domain.Entities.Cadastros;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Data.Context
{
    public class LivrariaDataContext : DbContext
    {
        public LivrariaDataContext(DbContextOptions<LivrariaDataContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Log> Logs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genero>().HasData(
               new Genero
               {
                   Id = 1,
                   Descricao = "Romance"
               });

            modelBuilder.Entity<Genero>().HasData(
               new Genero
               {
                   Id = 2,
                   Descricao = "Ficção científica"
               });

            modelBuilder.Entity<Genero>().HasData(
               new Genero
               {
                   Id = 3,
                   Descricao = "Horror"
               });

            modelBuilder.Entity<Genero>().HasData(
               new Genero
               {
                   Id = 4,
                   Descricao = "Fantasia"
               });

            modelBuilder.Entity<Genero>().HasData(
               new Genero
               {
                   Id = 5,
                   Descricao = "Poesia"
               });
        }
    }
}
