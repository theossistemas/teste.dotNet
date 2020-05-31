using Livraria.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Data.Maps
{
    public class LivroMap : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Livro");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.NomeLivro)
                .IsRequired()
                .HasColumnName("NomeLivro")
                .HasMaxLength(200)
                .HasColumnType("varchar(120)");

            builder.Property(x => x.Autor)
                .IsRequired()
                .HasColumnName("Autor")
                .HasMaxLength(200)
                .HasColumnType("varchar(120)");

            builder.Property(x => x.Editora)
                .IsRequired()
                .HasColumnName("Editora")
                .HasMaxLength(200)
                .HasColumnType("varchar(120)");
        }
    }
}
