using Livraria.Common.Utils;
using Livraria.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Data.Mappings
{
    public class AutorMapping : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(Constantes.QuantidadeDeCaracteres100);

            builder.HasMany(x => x.Livros)
                .WithOne(x => x.Autor)
                .HasForeignKey(x => x.AutorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(x => x.CascadeMode);
            builder.Ignore(x => x.ValidationResult);

            builder.ToTable(nameof(Autor));
        }
    }
}
