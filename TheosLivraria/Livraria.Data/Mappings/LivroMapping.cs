using Livraria.Common.Utils;
using Livraria.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Data.Mappings
{
    public class LivroMapping : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Titulo)
                .IsRequired()
                .HasMaxLength(Constantes.QuantidadeDeCaracteres200);

            builder.Property(x => x.AnoDePublicacao)
               .IsRequired();

            builder.Property(x => x.Edicao)
              .IsRequired();

            builder.HasOne(x => x.Autor)
                .WithMany(x => x.Livros)
                .HasForeignKey(x => x.AutorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(x => x.CascadeMode);
            builder.Ignore(x => x.ValidationResult);

            builder.ToTable(nameof(Livro));
        }
    }
}
