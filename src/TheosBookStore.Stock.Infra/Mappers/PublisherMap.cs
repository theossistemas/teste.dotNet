using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheosBookStore.Stock.Infra.Models;

namespace TheosBookStore.Stock.Infra.Mappers
{
    public class PublisherMap : IEntityTypeConfiguration<PublisherModel>
    {
        public void Configure(EntityTypeBuilder<PublisherModel> builder)
        {
            builder.ToTable("Publisher");
            builder.HasKey(p => p.Id);
        }
    }
}
