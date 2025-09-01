
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheosLivraria.Domain.Models.Entities;

namespace TheosLivraria.INFRA.Mappings
{
    public class RolesMap : IEntityTypeConfiguration<RolesEntity>
    {
        public void Configure(EntityTypeBuilder<RolesEntity> builder)
        {
            
            builder.ToTable("Roles", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Description)
                .HasColumnType("varchar")
                .IsRequired(false)
                     .HasMaxLength(500);

            builder.Property(x => x.Active)
               .HasColumnType("bit")
                  .IsRequired();

            builder.Property(x => x.CreatedDate)
                    .HasColumnType("datetime")
                     .IsRequired();

            builder.Property(x => x.ModifiedDate)
                    .HasColumnType("datetime")
                     .IsRequired();
        }
    }
}
