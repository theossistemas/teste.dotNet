using LivrariaTheos.Estoque.Domain.Generos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LivrariaTheos.Estoque.Data.Mapping
{
    public class GeneroMapping : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder.ToTable("Genero");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(t => t.Ativo)
              .HasDefaultValue(true)
              .IsRequired();

            builder.HasMany(c => c.Livros)
            .WithOne(p => p.Genero)
            .HasForeignKey(p => p.GeneroId);

            builder.Ignore(t => t.CascadeMode);

            builder.HasData(
              new
              {
                  Id = 1,
                  Nome = "Drama",
                  Ativo = true,
                  UsuarioInclusao = "Seed",
                  DataInclusao = new DateTime(2021, 1, 1, 1, 1, 1)
              },
              new
              {
                  Id = 2,
                  Nome = "Mistério/Suspense",
                  Ativo = true,
                  UsuarioInclusao = "Seed",
                  DataInclusao = new DateTime(2021, 1, 1, 1, 1, 1)
              },
              new
              {
                  Id = 3,
                  Nome = "Romance",
                  Ativo = true,
                  UsuarioInclusao = "Seed",
                  DataInclusao = new DateTime(2021, 1, 1, 1, 1, 1)
              },
              new
              {
                  Id = 4,
                  Nome = "Épico/Aventura",
                  Ativo = true,
                  UsuarioInclusao = "Seed",
                  DataInclusao = new DateTime(2021, 1, 1, 1, 1, 1)
              },
              new
              {
                  Id = 5,
                  Nome = "Distopia",
                  Ativo = true,
                  UsuarioInclusao = "Seed",
                  DataInclusao = new DateTime(2021, 1, 1, 1, 1, 1)
              },
              new
              {
                  Id = 6,
                  Nome = "Fantasia/Sobrenatural",
                  Ativo = true,
                  UsuarioInclusao = "Seed",
                  DataInclusao = new DateTime(2021, 1, 1, 1, 1, 1)
              },
              new
              {
                  Id = 7,
                  Nome = "Terror",
                  Ativo = true,
                  UsuarioInclusao = "Seed",
                  DataInclusao = new DateTime(2021, 1, 1, 1, 1, 1)

              });
        }
    }
}