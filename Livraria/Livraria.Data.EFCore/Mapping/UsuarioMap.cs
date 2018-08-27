using Livraria.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Data.EFCore.Mapping
{
    public class UsuarioMap : EntidadeBaseMap<Usuario>
    {
        public override void Configure(EntityTypeBuilder<Usuario> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Login);
            builder.Property(x => x.Senha);
        }
    }
}
