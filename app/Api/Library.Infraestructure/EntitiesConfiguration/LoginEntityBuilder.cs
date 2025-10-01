using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infraestructure.EntitiesConfiguration;

public class LoginEntityBuilder : IEntityTypeConfiguration<Login>
{
    public void Configure(EntityTypeBuilder<Login> builder)
    {
        builder.ToTable("Logins");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("ID");

        builder.Property(x => x.UserName).HasColumnName("USER_NAME").IsRequired();
        builder.HasIndex(x => x.UserName).IsUnique();
        builder.Ignore("_email");
        builder.Property(x => x.Email).HasColumnName("EMAIL").IsRequired();
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.PasswordHash).HasColumnName("PASSWORD_HASH").HasMaxLength(60).IsRequired();
        builder.Property(x => x.Role).HasColumnName("ROLE").IsRequired();
        builder.Property(x => x.Active).HasColumnName("ACTIVE").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("CREATED_AT").IsRequired();
    }
}