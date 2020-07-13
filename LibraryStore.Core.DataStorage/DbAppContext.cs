using LibraryStore.Core.Data.Entities;
using LibraryStore.Core.DataStorage.DataSeeding;
using Microsoft.EntityFrameworkCore;

namespace LibraryStore.Core.DataStorage
{
    public class DbAppContext : DbContext, IDbAppContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        public DbAppContext(DbContextOptions<DbAppContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=LibraryStore;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbAppContext).Assembly);
            modelBuilder.Seed();
        }
    }
}