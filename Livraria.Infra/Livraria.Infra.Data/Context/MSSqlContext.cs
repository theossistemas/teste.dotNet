using System;
using System.Linq;
using Livraria.Domain.Entities;
using Livraria.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Data.Context
{
    public class MSSqlContext : DbContext
    {
        public MSSqlContext(DbContextOptions<MSSqlContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;            
        }

        public DbSet<Livro> Livros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Livro>(new LivroMap().Configure);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                            .Entries()
                            .Where(e => e.Entity is BaseEntity && (
                                    e.State == EntityState.Added
                                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {

                ((BaseEntity)entityEntry.Entity).DataAtualizacao = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).DataCriacao = DateTime.Now;
                }
                else{
                    entityEntry.Property("DataAtualizacao").IsModified = false;
                }
            }
            return base.SaveChanges();
        }
    }
}