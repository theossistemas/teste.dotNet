using LibraryStore.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryStore.Core.DataStorage.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Fullname).HasMaxLength(150).IsRequired();
            builder.Property(p => p.Username).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Password).HasMaxLength(50).IsRequired();
            builder.Property(p => p.CreatedAt).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.UpdatedAt).ValueGeneratedOnUpdate();
            builder.Property(p => p.Active).IsRequired();
        }
    }
}