using Microsoft.EntityFrameworkCore;
using TheosBookStore.Stock.Infra.Mappers;
using TheosBookStore.Stock.Infra.Models;

namespace TheosBookStore.Stock.Infra.Context
{
    public class TheosBookStoreStockDbContext : DbContext
    {
        public DbSet<AuthorModel> Authors { get; set; }
        public DbSet<PublisherModel> Publishers { get; set; }
        public DbSet<BookModel> Books { get; set; }
        public DbSet<BookAuthor> BookAuthor { get; set; }

        public TheosBookStoreStockDbContext() : base() { }
        public TheosBookStoreStockDbContext(DbContextOptions<TheosBookStoreStockDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorMap());
            modelBuilder.ApplyConfiguration(new BookAuthorMap());
            modelBuilder.ApplyConfiguration(new PublisherMap());
            modelBuilder.ApplyConfiguration(new BookMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
