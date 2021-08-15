using LivrariaTheos.Estoque.Domain.Livros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LivrariaTheos.Estoque.Data.Mapping
{
    public class LivroMapping : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Livro");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Sinopse)
                .IsRequired()
                .HasColumnType("varchar(2000)");

            builder.Property(c => c.QuantidadePaginas)
               .IsRequired();

            builder.Property(c => c.CaminhoCapa).HasColumnType("varchar(500)");
            builder.Property(c => c.NomeCapa).HasColumnType("varchar(150)");

            builder.Property(t => t.Ativo)
              .HasDefaultValue(true)
              .IsRequired();

            builder.Ignore(t => t.CascadeMode);

            builder.HasData(
              new
              {
                  Id = 1,
                  Nome = "Harry Potter e o Prisioneiro de Azkaban",
                  Sinopse = "Sinopse de Harry Potter",
                  QuantidadePaginas = 250,
                  CaminhoCapa = "StaticFiles\\Capas",
                  NomeCapa = "ID_1",
                  Ativo = true,
                  AutorId = 5,
                  GeneroId = 4,
                  UsuarioInclusao = "Seed",
                  DataInclusao = new DateTime(2021, 1, 1, 1, 1, 1)
              },
              new
              {
                  Id = 2,
                  Nome = "A Montanha Mágica",
                  Sinopse = "Sinopse de a Montanha Mágica",
                  QuantidadePaginas = 250,
                  CaminhoCapa = "StaticFiles\\Capas",
                  NomeCapa = "ID_2",
                  Ativo = true,
                  AutorId = 4,
                  GeneroId = 1,
                  UsuarioInclusao = "Seed",
                  DataInclusao = new DateTime(2021, 1, 1, 1, 1, 1)
              },
              new
              {
                  Id = 3,
                  Nome = "O Iluminado",
                  Sinopse = "Sinopse de O Iluminado",
                  QuantidadePaginas = 250,
                  CaminhoCapa = "StaticFiles\\Capas",
                  NomeCapa = "ID_1",
                  Ativo = true,
                  AutorId = 1,
                  GeneroId = 5,
                  UsuarioInclusao = "Seed",
                  DataInclusao = new DateTime(2021, 1, 1, 1, 1, 1)
              });
        }
    }
}