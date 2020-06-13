using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MMM.IStore.Core.Messages;
using MMM.Library.Domain.Core.Data;
using MMM.Library.Domain.Models;
using MMM.Library.Infra.Data.DataSeeders;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MMM.Library.Infra.Data.Context
{
    public class LibraryDbContext : DbContext, IUnitOfWork
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)"); // breaking changes EF Core 3.1 // v2.2 => property.Relational().ColumnType = "varchar(100)";

            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
               .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.HasSequence<int>("CategoryCode").StartsAt(1000).IncrementsBy(1);

            DummyDataSeeder.CategoryDataSeeder(modelBuilder);
        }


        public async Task<bool> Commit()
        {
            var sucess = await base.SaveChangesAsync() > 0;
            return sucess;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }


        // DesignTimeDbContextFactory https://go.microsoft.com/fwlink/?linkid=851728
        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LibraryDbContext>
        {
            public LibraryDbContext CreateDbContext(string[] args)
            {
                //    IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(System.AppContext.BaseDirectory).AddJsonFile("appsettings.Production.json").Build();

                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(@Directory.GetCurrentDirectory() + "/../MMM.Library.Services.AspNetWebApi/appsettings.Development.json")
                    .Build();

                var builder = new DbContextOptionsBuilder<LibraryDbContext>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                builder.UseSqlServer(connectionString);

                return new LibraryDbContext(builder.Options);
            }
        }
    }
}
