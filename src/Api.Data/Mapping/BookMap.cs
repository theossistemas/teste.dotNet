using System.Collections.Immutable;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class BookMap : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Year);
            builder.Property(b => b.CreatedAt);
            builder.Property(b => b.UpdatedAt);
            builder.Property(b => b.Title);
            builder.Property(b => b.Author).HasMaxLength(50);
            builder.Property(b => b.Edition).HasMaxLength(50);
            builder.Property(b => b.Publishing).HasMaxLength(60);
            builder.Property(b => b.Language).HasMaxLength(60);

            builder.HasOne(b => b.User)
                   .WithMany(u => u.Books)
                   .HasForeignKey(b => b.IncludedBy);
        }
    }
}
