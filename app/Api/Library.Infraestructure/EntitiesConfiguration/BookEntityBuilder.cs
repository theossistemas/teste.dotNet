using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infraestructure.EntitiesConfiguration;

public class BookEntityBuilder : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("BOOKS");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("ID");

        builder.Property(x => x.Name).HasColumnName("NAME").HasMaxLength(200).IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Author).HasColumnName("AUTHOR").HasMaxLength(150).IsRequired();
        builder.Property(x => x.Summary).HasColumnName("SUMMARY").HasMaxLength(1000).IsRequired();
        builder.Property(x => x.Active).HasColumnName("ACTIVE").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("CREATED_AT").IsRequired();
    }
}