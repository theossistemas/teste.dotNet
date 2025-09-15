using Livraria.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Title).IsRequired().HasMaxLength(200);
                b.Property(e => e.Author).IsRequired().HasMaxLength(150);
                b.HasIndex(e => e.Title).IsUnique();
            });

            modelBuilder.Entity<User>(u =>
            {
                u.HasKey(e => e.Id);
                u.Property(e => e.Username).IsRequired().HasMaxLength(100);
                u.Property(e => e.PasswordHash).IsRequired();
                u.Property(e => e.Role).IsRequired().HasMaxLength(50);
                u.HasIndex(e => e.Username).IsUnique();
            });
        }
    }
}