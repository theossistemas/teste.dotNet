using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LivrariaJc.Domain.Entidades;
using LivrariaJc.Data.Seeds;

namespace LivrariaJc.Data.EntityBuilders
{
    public class LivroMapping : IEntityTypeConfiguration<LivrosEntidade>
    {
        public void Configure(EntityTypeBuilder<LivrosEntidade> builder)
        {
            builder.ToTable("Livros");

            builder.HasKey(x => x.Id);

            builder.Property(f => f.Autor)
            .HasMaxLength(140);

            builder.Property(f => f.Titulo)
           .HasMaxLength(250);

            builder.Property(x => x.Valor)
            .HasPrecision(10, 4);

            LivroSeeds.Livros(builder);
        }
    }
}