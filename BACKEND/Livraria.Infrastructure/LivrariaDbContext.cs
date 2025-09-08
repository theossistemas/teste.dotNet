using Livraria.Domain;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infrastructure
{
    public class LivrariaDbContext : DbContext
    {
        public LivrariaDbContext(DbContextOptions<LivrariaDbContext> options) : base(options) { }
        public DbSet<Livro> Livros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Livro>(entity =>
            {
                modelBuilder.Entity<Livro>().ToTable("Livraria");

                entity.HasKey(e => e.Id); // declarando a chave primária

                entity.Property(e => e.Titulo)
                    .IsRequired();

                entity.Property(e => e.Autor)
                    .IsRequired();

                entity.Property(e => e.ISBN)
                    .IsRequired();

                entity.Property(e => e.AnoPublicacao)
                    .IsRequired();

                entity.Property(e => e.Quantidade)
                    .IsRequired();

                entity.Property(e => e.Preco)
                    .IsRequired()
                    .HasPrecision(18, 2); // 18 dígitos, 2 casas decimais

            });
        }
    }
}
