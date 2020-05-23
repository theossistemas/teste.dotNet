using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheosBookStore.Stock.Infra.Models;

namespace TheosBookStore.Stock.Infra.Mappers
{
    public class BookAuthorMap : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.ToTable("BookAuthor");
            builder.HasKey(ba => new { ba.AuthorId, ba.BookId });
            builder
                .HasOne(ba => ba.Book)
                .WithMany(b => b.Authors)
                .HasForeignKey(ba => ba.BookId);
            builder
                .HasOne(ba => ba.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(ba => ba.AuthorId);
        }
    }
}
