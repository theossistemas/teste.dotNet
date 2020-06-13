using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MMM.Library.Infra.CrossCutting.Identity.DbContext;
using MMM.Library.Infra.Data.Context;
using MMM.Library.Infra.Data.EventSourcing;
using System;

namespace MMM.Library.Services.AspNetWebApi.Configurations
{
    public static class DatabaseSetup
    {
        public static void AddDatabaseSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
              b => b.MigrationsAssembly("MMM.Library.Infra.Data"))); 

            services.AddDbContext<EventSourcingDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
              b => b.MigrationsAssembly("MMM.IStore.Infra.Data"))); 

            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
              b => b.MigrationsAssembly("MMM.IStore.Infra.Data")));
        }
    }
}
