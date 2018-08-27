using Livraria.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Data.EFCore.Mapping
{
    public class AutorMap : EntidadeBaseMap<Autor>
    {
        public override void Configure(EntityTypeBuilder<Autor> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Nome);
        }
    }
}
