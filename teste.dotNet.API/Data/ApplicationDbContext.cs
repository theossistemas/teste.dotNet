using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using teste.dotNet.API.Entities;

namespace teste.dotNet.API.Data {
    public class ApplicationDbContext : DbContext {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
        public DbSet<Book> Books { get; set; }
        public DbSet<Writer> Writers {get; set;}
        public DbSet<BookWriter> BookWriters {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookWriter>().HasKey(bookWriter => new {bookWriter.BookId, bookWriter.WriterId});
            modelBuilder.Entity<Book>().HasIndex(book => book.Title).IsUnique();
            modelBuilder.Entity<Writer>().HasIndex(book => book.Name).IsUnique();
        }   
    }    
}