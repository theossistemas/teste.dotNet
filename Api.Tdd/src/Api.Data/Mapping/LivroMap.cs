using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class LivroMap : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Livro");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Titulo).HasMaxLength(100);
            builder.Property(c => c.Emissora).HasMaxLength(100);
            builder.Property(c => c.Autor).HasMaxLength(100);
            builder.Property(c => c.Categoria).HasMaxLength(100);
            builder.Property(c => c.DataLancamento).IsRequired();
            builder.Property(c => c.Valor).IsRequired();
            builder.Property(c => c.Quantidade).IsRequired();
        }
    }
}

