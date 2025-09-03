using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheosLivraria.Domain.Models.Entities;

namespace TheosLivraria.Infrastructure.Data.Configurations;

public class BookEntityConfiguration : IEntityTypeConfiguration<BookEntity>
{
    public void Configure(EntityTypeBuilder<BookEntity> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.Author)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Description)
            .HasMaxLength(600);

        builder.Property(b => b.PublishedDate)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(b => b.Price)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.Property(b => b.StockQuantity)
            .IsRequired();

        builder.Property(b => b.IsActive)
            .HasDefaultValue(true);

        builder.Property(b => b.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(b => b.UpdatedAt);

        builder.HasIndex(b => b.Title);
        builder.HasIndex(b => b.Author);

    }
}
