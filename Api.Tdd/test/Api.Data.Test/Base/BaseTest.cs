using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Api.Data.Test
{
    public abstract class BaseTest
    {
        public void BaseTeste()
        {

        }
    }
    public class DbTeste : IDisposable
    {        
        public ServiceProvider serviceProvider { get; private set; }
        private string dataBaseNome = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
        public DbTeste()
        {           
            var con = new ConnectionStringManager();
            var connectionString = con.GetConnectionString();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<MyContext>(c =>
            c.UseSqlServer($"Data Source=DESKTOP-CVF6VA6\\SQLEXPRESS;Initial Catalog={dataBaseNome};Persist Security Info=True;User ID=sa;Password=Dsm22"),
            ServiceLifetime.Transient
            );
            serviceProvider = serviceCollection.BuildServiceProvider();
            using (var context = serviceProvider.GetService<MyContext>())
            {
                context.Database.EnsureCreated();
            }

        }


        public void Dispose()
        {
            using (var context = serviceProvider.GetService<MyContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
