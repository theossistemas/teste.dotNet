using Microsoft.EntityFrameworkCore;
using LivrariaWeb.Domain.Model;

namespace LivrariaWeb.Infra.Map
{
    class LivroMap : IEntityTypeConfiguration<Livro>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Livro");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.DataPublicacao).HasColumnName("data_publicacao").IsRequired();
            builder.Property(x => x.NomeLivro).HasColumnName("nome_livro").IsRequired();
            builder.Property(x => x.NomeAutor).HasColumnName("nome_autor").IsRequired();
            builder.Property(x => x.NumeroPaginas).HasColumnName("numero_paginas");
        }
    }
}
