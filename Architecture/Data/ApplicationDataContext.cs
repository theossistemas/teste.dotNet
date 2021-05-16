using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Architecture
{
    public class ApplicationDataContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Book> Books { get; set; }

        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEntityMapping).Assembly);
        }

        public override int SaveChanges()
        {
            ManagerDomainTracker();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            ManagerDomainTracker();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ManagerDomainTracker()
        {
            ChangeTracker.DetectChanges();

            var changesTracked = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted
                || x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var item in changesTracked)
            {
                UpdateLog(item);
            }
        }

        private void UpdateLog(EntityEntry item)
        {
            if (item.Entity is BaseDomain entity)
            {
                switch (item.State)
                {
                    case EntityState.Modified:
                        entity.Updated = DateTime.Now;
                        break;
                    case EntityState.Added:
                        entity.Created = DateTime.Now;
                        break;
                }
            }
        }
    }
}
