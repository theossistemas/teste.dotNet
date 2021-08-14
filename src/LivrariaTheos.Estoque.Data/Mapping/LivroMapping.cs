using LivrariaTheos.Estoque.Domain.Livros;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LivrariaTheos.Estoque.Data.Mapping
{
    public class LivroMapping : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Sinopse)
                .IsRequired()
                .HasColumnType("varchar(2000)");

            builder.Property(c => c.QuantidadePaginas)
               .IsRequired();

            builder.Property(c => c.CaminhoCapa).HasColumnType("varchar(350)");

            builder.Property(t => t.Ativo)
              .HasDefaultValue(true)
              .IsRequired();

            builder.Ignore(t => t.CascadeMode);

            builder.ToTable("Livro");
        }
    }
}