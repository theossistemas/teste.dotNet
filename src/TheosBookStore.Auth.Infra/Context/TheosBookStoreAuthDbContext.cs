using Microsoft.EntityFrameworkCore;

using TheosBookStore.Auth.Infra.Mapping;
using TheosBookStore.Auth.Infra.Models;

namespace TheosBookStore.Auth.Infra.Context
{
    public class TheosBookStoreAuthDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public TheosBookStoreAuthDbContext() : base() { }
        public TheosBookStoreAuthDbContext(DbContextOptions<TheosBookStoreAuthDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
