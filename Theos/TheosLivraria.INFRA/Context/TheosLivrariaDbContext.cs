
using Microsoft.EntityFrameworkCore;
using TheosLivraria.Domain.Models.Entities;
using TheosLivraria.Domain.Models.Enum;

namespace TheosLivraria.INFRA.Context
{
    public class TheosLivrariaDbContext(DbContextOptions<TheosLivrariaDbContext> options)
        : DbContext(options)
    {
        public DbSet<BookEntity> Book { get; set; }
        public DbSet<StatusEntity> Status { get; set; }
        public DbSet<AuditLogEntity> AuditLogs { get; set; }
        public DbSet<UserEntity> User { get; set; }
        public DbSet<RolesEntity> Role { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TheosLivrariaDbContext).Assembly);

            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    // Se for string e não tiver MaxLength definido, define varchar(100)
                    if (property.ClrType == typeof(string)
                        && property.GetMaxLength() == null
                        && string.IsNullOrEmpty(property.GetColumnType()))
                    {
                        property.SetMaxLength(100);
                        property.SetColumnType("varchar(100)");
                    }
                }
            }

            modelBuilder.Entity<RolesEntity>().HasData(
                new RolesEntity { Id = 1, Name = "Admin", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
                new RolesEntity { Id = 2, Name = "Public", CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now }
                );

            modelBuilder.Entity<StatusEntity>().HasData(
                new StatusEntity { Id = 1, Name = "Disponivel", Type = (int)StatusTypeEnum.Book, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
                new StatusEntity { Id = 2, Name = "Emprestado", Type = (int)StatusTypeEnum.Book, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
                new StatusEntity { Id = 3, Name = "Reservado", Type = (int)StatusTypeEnum.Book, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now }
            );
        }

        public override int SaveChanges()
        {
            ApplyAuditInfo();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditInfo();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditInfo()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity &&
                           (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = DateTime.Now;
                    entity.ModifiedDate = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.ModifiedDate = DateTime.Now;
                }
            }
        }
    }
}
