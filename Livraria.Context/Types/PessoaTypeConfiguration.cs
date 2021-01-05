using Livraria.Domain.Pessoas;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Context.Types
{
    public class PessoaTypeConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasMany(a => a.Livros).WithOne(l => l.Autor).HasForeignKey(l => l.IdAutor);

            builder.HasData(new Pessoa { Id = 1, Nome = "Administrador" });
        }
    }
}
