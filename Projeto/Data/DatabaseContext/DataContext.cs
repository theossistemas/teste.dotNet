using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Data.DatabaseContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasData(new User(1, "Admin", "admin123"));
        }


        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
