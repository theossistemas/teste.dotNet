using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheoLivraria.Dominio.Entidades;

namespace TheoLivraria.Infra.Contexto.Maps
{
    public class LivroMap : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Livros");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(200).HasColumnType("varchar(200)");
            builder.Property(x => x.Editora).IsRequired().HasMaxLength(100).HasColumnType("varchar(100)");
        }
    }
}
