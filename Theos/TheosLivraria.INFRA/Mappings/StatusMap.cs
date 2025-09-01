
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheosLivraria.Domain.Models.Entities;

namespace TheosLivraria.INFRA.Mappings
{
    public class StatusMap : IEntityTypeConfiguration<StatusEntity>
    {
        public void Configure(EntityTypeBuilder<StatusEntity> builder)
        {
            builder.ToTable("Status", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Description)
                .HasColumnType("varchar")
                     .IsRequired(false)
                     .HasMaxLength(500);

            builder.Property(x => x.Type)
                    .HasColumnType("int")
                     .IsRequired();

            builder.Property(x => x.CreatedDate)
                    .HasColumnType("datetime")
                     .IsRequired();

            builder.Property(x => x.ModifiedDate)
                    .HasColumnType("datetime")
                     .IsRequired();

            builder.Property(x => x.Active)
               .HasColumnType("bit")
                  .IsRequired();
        }
    }
}
