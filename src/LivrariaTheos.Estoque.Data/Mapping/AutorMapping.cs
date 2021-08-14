using LivrariaTheos.Estoque.Domain.Autores;
using LivrariaTheos.Estoque.Domain.Autores.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LivrariaTheos.Estoque.Data.Mapping
{
    public class AutorMapping : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.ToTable("Autor");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.InformacoesRelevantes)               
                .HasColumnType("varchar(2000)");

            builder.Property(c => c.Nacionalidade)
               .IsRequired();

            builder.Property(t => t.Ativo)
              .HasDefaultValue(true)
              .IsRequired();

            builder.HasMany(c => c.Livros)
            .WithOne(p => p.Autor)
            .HasForeignKey(p => p.AutorId);

            builder.Ignore(t => t.CascadeMode);

            builder.HasData(
              new
              {
                  Id = 1,
                  Nome = "Stephen King",
                  Nacionalidade = (int)NacionalidadeEnum.EstadosUnidos,
                  InformacoesRelevantes = "Informações sobre o autor",
                  Ativo = true,
                  UsuarioInclusao = "Seed",
                  DataInclusao = new DateTime(2021, 1, 1, 1, 1, 1)
              },
              new
              {
                  Id = 2,
                  Nome = "Paulo Coelho",
                  Nacionalidade = (int)NacionalidadeEnum.Brasil,
                  InformacoesRelevantes = "Informações sobre o autor",
                  Ativo = true,
                  UsuarioInclusao = "Seed",
                  DataInclusao = new DateTime(2021, 1, 1, 1, 1, 1)
              }, 
              new
              {
                  Id = 3,
                  Nome = "Akira Toryama",
                  Nacionalidade = (int)NacionalidadeEnum.Japao,
                  InformacoesRelevantes = "Informações sobre o autor",
                  Ativo = true,
                  UsuarioInclusao = "Seed",
                  DataInclusao = new DateTime(2021, 1, 1, 1, 1, 1)
              }, 
              new
              {
                  Id = 4,
                  Nome = "Thomas Mann",
                  Nacionalidade = (int)NacionalidadeEnum.Alemanha,
                  InformacoesRelevantes = "Informações sobre o autor",
                  Ativo = true,
                  UsuarioInclusao = "Seed",
                  DataInclusao = new DateTime(2021, 1, 1, 1, 1, 1)
              },
              new
              {
                  Id = 5,
                  Nome = "J.K Rowling",
                  Nacionalidade = (int)NacionalidadeEnum.ReinoUnido,
                  InformacoesRelevantes = "Informações sobre o autor",
                  Ativo = true,
                  UsuarioInclusao = "Seed",
                  DataInclusao = new DateTime(2021, 1, 1, 1, 1, 1)
              });
        }
    }
}
