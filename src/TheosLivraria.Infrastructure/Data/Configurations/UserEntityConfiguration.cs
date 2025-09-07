using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheosLivraria.Domain.Models.Entities;
using TheosLivraria.Domain.Models.Enums;

namespace TheosLivraria.Infrastructure.Data.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users", "dbo");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.Role)
               .HasConversion<string>()
               .HasMaxLength(50)
               .IsRequired()
               .HasDefaultValue(RolesEnum.User);

        builder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(u => u.UpdatedAt);

        builder.HasIndex(u => u.Email).IsUnique();
    }
}
