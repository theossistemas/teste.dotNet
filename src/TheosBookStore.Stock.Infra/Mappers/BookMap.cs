using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheosBookStore.Stock.Infra.Models;

namespace TheosBookStore.Stock.Infra.Mappers
{
    public class BookMap : IEntityTypeConfiguration<BookModel>
    {
        public void Configure(EntityTypeBuilder<BookModel> builder)
        {
            builder.ToTable("Book");
            builder.HasIndex(book => book.ISBN);
        }
    }
}
