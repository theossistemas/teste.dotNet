using LivrariaTheos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LivrariaTheos.Infrastructure.Data;

public class LivrariaTheosDbContext : DbContext
{
    public LivrariaTheosDbContext(DbContextOptions<LivrariaTheosDbContext> options)
        : base(options)
    {
    }

    public DbSet<Livro> Livros { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Livro>(entity =>
{
    entity.HasKey(e => e.Id);

    entity.Property(e => e.Titulo)
          .IsRequired()
          .HasMaxLength(200);

    entity.Property(e => e.Autor)
          .IsRequired()
          .HasMaxLength(150);

    entity.Property(e => e.AnoPublicacao); // int não-nullable já é "required" por tipo

    entity.Property(e => e.ISBN)
          .HasMaxLength(20);

    entity.Property(e => e.Editora)
          .HasMaxLength(100);

    entity.Property(e => e.DataPublicacao)
          .IsRequired()
          .HasColumnType("date");

    entity.Property(e => e.Preco)
          .IsRequired()
          .HasPrecision(18, 2);

    entity.Property(e => e.Estoque)
          .IsRequired();

    // Unicidade
    entity.HasIndex(e => new { e.Titulo, e.Autor, e.AnoPublicacao }).IsUnique();
    entity.HasIndex(e => e.ISBN).IsUnique().HasFilter("[ISBN] IS NOT NULL");

    // Check constraints (opcional)
    entity.ToTable(t =>
    {
        t.HasCheckConstraint("CK_Livro_Preco_NaoNegativo", "[Preco] >= 0");
        t.HasCheckConstraint("CK_Livro_Estoque_NaoNegativo", "[Estoque] >= 0");
    });

    // Índice auxiliar para ordenação/paginação por título
    entity.HasIndex(e => e.Titulo).HasDatabaseName("IX_Livro_Titulo");
});

    }
}


