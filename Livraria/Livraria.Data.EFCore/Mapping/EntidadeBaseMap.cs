using Livraria.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Data.EFCore.Mapping
{
    public class EntidadeBaseMap<T> : IEntityTypeConfiguration<T> where T : EntidadeBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Created);
            builder.Property(x => x.Updated);
        }
    }
}
