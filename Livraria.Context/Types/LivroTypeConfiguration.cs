using Livraria.Domain.Livros;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Context.Types
{
    public class LivroTypeConfiguration : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.HasKey(l => l.Id);

            builder.HasMany(l => l.Autores).WithOne(a => a.Livro).HasForeignKey(a => a.IdLivro);
            builder.HasMany(l => l.Temas).WithOne(a => a.Livro).HasForeignKey(a => a.IdLivro);
        }
    }
}
