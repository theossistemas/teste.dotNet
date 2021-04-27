using System.Linq;
using Livraria.Domain.Security.Entities;
using Livraria.Infra.Data.Security.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Data.Security.Contex
{
    public class SecurityDbContext : DbContext
    {
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
            
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(new UserMap().Configure);
        }

        // public override int SaveChanges()
        // {
        //     var entries = ChangeTracker
        //                     .Entries()
        //                     .Where(e => e.Entity is User && (
        //                             e.State == EntityState.Added
        //                             || e.State == EntityState.Modified));

        //     foreach (var entityEntry in entries)
        //     {

        //         ((BaseEntity)entityEntry.Entity).DataAtualizacao = DateTime.Now;

        //         if (entityEntry.State == EntityState.Added)
        //         {
        //             ((BaseEntity)entityEntry.Entity).DataCriacao = DateTime.Now;
        //         }
        //         else{
        //             entityEntry.Property("DataAtualizacao").IsModified = false;
        //         }
        //     }
        //     return base.SaveChanges();
        // }
    }
}