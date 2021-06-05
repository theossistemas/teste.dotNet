using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteDotNet.Business.Models;

namespace TesteDotNet.Data.Mappings
{
    public class LivroMapping : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Autor)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Categoria)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Edicao)
                .HasColumnType("varchar(50)");

            builder.ToTable("Livros");
        }
    }
}
