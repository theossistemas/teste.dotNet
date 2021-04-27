using Livraria.Domain.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.Infra.Data.Security.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
           .UseIdentityColumn()
           .HasColumnName("Id");

            builder.Property(p => p.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("varchar(50)");

            builder.Property(p => p.Login)
            .IsRequired()
            .HasColumnName("Login")
            .HasColumnType("varchar(50)");

            builder.Property(p => p.Password)
            .IsRequired()
            .HasColumnName("Password")
            .HasColumnType("varchar(70)");

            builder.Property(p => p.Role)
            .IsRequired()
            .HasColumnName("Role")
            .HasColumnType("varchar(10)");
        }
    }
}