using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MMM.Library.Domain.Core.EvetSourcing;
using MMM.Library.Infra.Data.Mappings;
using System.IO;
using System.Linq;

namespace MMM.Library.Infra.Data.EventSourcing
{
    public class EventSourcingDbContext : DbContext
    {
        public EventSourcingDbContext(DbContextOptions<EventSourcingDbContext> options)
            : base(options) { }
        public DbSet<StoredEvent> StoredEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StoredEventMap());
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventSourcingDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EventSourcingDbContext>
        {
            public EventSourcingDbContext CreateDbContext(string[] args)
            {
                //    IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(System.AppContext.BaseDirectory).AddJsonFile("appsettings.Production.json").Build();

                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(@Directory.GetCurrentDirectory() + "/../MMM.Library.Services.AspNetWebApi/appsettings.Development.json")
                    .Build();

                var builder = new DbContextOptionsBuilder<EventSourcingDbContext>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                builder.UseSqlServer(connectionString);

                return new EventSourcingDbContext(builder.Options);
            }
        }
    }
}
