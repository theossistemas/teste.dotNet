using LibraryStore.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryStore.Core.DataStorage.Configurations
{
    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Title).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(4000).IsRequired();
            builder.Property(p => p.Author).HasMaxLength(100).IsRequired();
            builder.Property(p => p.UrlImage).HasMaxLength(400);
            builder.Property(p => p.CreatedAt).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.UpdatedAt).ValueGeneratedOnUpdate();
            builder.Property(p => p.Active).IsRequired();
        }
    }
}