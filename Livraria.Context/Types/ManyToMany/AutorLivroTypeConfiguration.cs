using Livraria.Domain.ManyToMany;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Context.Types.ManyToMany
{
    public class AutorLivroTypeConfiguration : IEntityTypeConfiguration<AutorLivro>
    {
        public void Configure(EntityTypeBuilder<AutorLivro> builder)
        {
            builder.HasKey(al => new { al.IdLivro, al.IdAutor });

            builder.HasOne(al => al.Autor).WithMany(a => a.Livros).HasForeignKey(al => al.IdAutor);
            builder.HasOne(al => al.Livro).WithMany(a => a.Autores).HasForeignKey(al => al.IdLivro);
        }
    }
}
