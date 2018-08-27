using Livraria.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Data.EFCore.Mapping
{
    public class EditoraMap : EntidadeBaseMap<Editora>
    {
        public override void Configure(EntityTypeBuilder<Editora> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Nome);
        }
    }
}
