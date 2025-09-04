using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheosLivraria.Domain.Entidades;
using TheosLivraria.Infra.Mapping.Base;

namespace TheosLivraria.Infra.Mapping
{
    public class LivroMapping : EntidadeBaseMapping<int, Livro>
    {
        public override void Map(EntityTypeBuilder<Livro> builder)
        {
            base.Map(builder);
            builder.ToTable("livros");

            builder.Property(l => l.Titulo)
                .IsRequired()
                .HasColumnName("titulo")
                .HasMaxLength(255);

            builder.Property(l => l.Autor)
                .IsRequired()
                .HasColumnName("autor")
                .HasMaxLength(100);

            builder.Property(l => l.Isbn)
                .IsRequired()
                .HasColumnName("isbn")
                .HasMaxLength(13);
            builder.HasIndex(l => l.Isbn).IsUnique();

            builder.Property(l => l.DataPublicacao)
                .IsRequired()
                .HasColumnName("data_publicacao");

            builder.Property(l => l.Preco)
                .IsRequired()
                .HasColumnName("preco")
                .HasColumnType("decimal(18,2)");

            builder.Property(l => l.Estoque)
                .IsRequired()
                .HasColumnName("estoque");
        }
    }
}
