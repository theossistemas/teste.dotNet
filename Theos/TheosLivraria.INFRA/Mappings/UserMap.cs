
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheosLivraria.Domain.Models.Entities;

namespace TheosLivraria.INFRA.Mappings
{
    public class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.Email)
               .HasColumnType("varchar")
                  .IsRequired()
                  .HasMaxLength(100);

            builder.Property(x => x.PasswordHash)
                .HasColumnType("varchar")
                    .IsRequired()
                    .HasMaxLength(300);

            builder.HasOne(x => x.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();

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
