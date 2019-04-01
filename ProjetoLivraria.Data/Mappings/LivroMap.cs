using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoLivraria.Domain.Entities;

namespace ProjetoLivraria.Data.Mappings
{
    public class LivroMap : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.Property(l => l.Id)
                .HasColumnName("Id");

            builder.Property(l => l.Titulo)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(l => l.Autor)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(l => l.Isbn)
                .HasColumnType("varchar(30)")
                .HasMaxLength(30)
                .IsRequired();
        }

    }
}
