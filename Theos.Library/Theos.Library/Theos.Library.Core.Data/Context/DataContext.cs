using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Theos.Library.CrossCutting;
using Theos.Library.Domain.Books;
using Theos.Library.Domain.Logs;
using Theos.Library.Domain.Users;

namespace Theos.Library.Core.Data.Context
{
    public class DataContext : DbContext
    {
        //Entities
        public DbSet<Book> Book { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Log> Log { get; set; }

        //Keys
        public DbSet<BookKey> BookKey { get; set; }
        public DbSet<UserKey> UserKey { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.UseSqlServer(EnvironmentProperties.StringConnection);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();
            modelBuilder.RemoveCascadeDeleteBehavior();

            modelBuilder.SetDateTimeColumnType();
        }
    }

    public static class ModelBuilderExtensions
    {
        public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
        {
            modelBuilder.Model.GetEntityTypes().ToList().ForEach(entity => entity.Relational().TableName = entity.DisplayName());
        }

        public static void RemoveCascadeDeleteBehavior(this ModelBuilder modelBuilder)
        {
            modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys())
                .Where(w => !w.IsOwnership && w.DeleteBehavior.Equals(DeleteBehavior.Cascade)).ToList()
                .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
        }

        public static void SetStringColumnType(this ModelBuilder modelBuilder, int length)
        {
            modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetProperties()).Where(w => w.ClrType == typeof(string)).ToList()
                .ForEach(property =>
                {
                    property.Relational().ColumnType = "varchar";
                    property.AsProperty().Builder.HasMaxLength(length, ConfigurationSource.DataAnnotation);
                });
        }

        public static void SetDateTimeColumnType(this ModelBuilder modelBuilder)
        {
            modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetProperties()).Where(w => w.ClrType == typeof(DateTime)).ToList()
                .ForEach(property => property.Relational().ColumnType = "datetime2");
        }
    }
}