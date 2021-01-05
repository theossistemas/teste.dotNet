using Livraria.Domain.Livros;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Context.Types
{
    public class TemaTypeConfiguration : IEntityTypeConfiguration<Tema>
    {
        public void Configure(EntityTypeBuilder<Tema> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasMany(t => t.Livros).WithOne(l => l.Tema).HasForeignKey(l => l.IdTema);
        }
    }
}
