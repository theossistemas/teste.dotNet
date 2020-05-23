using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheosBookStore.Auth.Domain.Entities;
using TheosBookStore.Auth.Domain.ValueObjects;
using TheosBookStore.Auth.Infra.Models;

namespace TheosBookStore.Auth.Infra.Mapping
{
    public class UserMap : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("user");

            builder
                .HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder
               .HasIndex(u => u.Email)
               .IsUnique();

            builder
                .Property(u => u.Hash)
                .IsRequired();

            builder
                .Property(u => u.Salt)
                .IsRequired();

            var userTable = GetUserTable();
            builder.HasData(userTable);
        }

        public UserModel GetUserTable()
        {
            var pass = new Password("admin");
            var user = new User("admin", "admin@admin.com", pass, "ADMIN");
            var userTable = new UserModel
            {
                Id = 1,
                Name = user.Name,
                Email = user.Email,
                Hash = user.Password.Hash,
                Salt = user.Password.Salt,
                Roles = user.Roles
            };
            return userTable;
        }
    }
}
