using Livraria.Domain.ManyToMany;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Context.Types.ManyToMany
{
    public class LivroTemaTypeConfiguration : IEntityTypeConfiguration<LivroTema>
    {
        public void Configure(EntityTypeBuilder<LivroTema> builder)
        {
            builder.HasKey(lt => new { lt.IdLivro, lt.IdTema });

            builder.HasOne(lt => lt.Livro).WithMany(l => l.Temas).HasForeignKey(lt => lt.IdLivro);
            builder.HasOne(lt => lt.Tema).WithMany(l => l.Livros).HasForeignKey(lt => lt.IdTema);
        }
    }
}
