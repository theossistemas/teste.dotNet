using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Teste.App.Services;
using Domain.Repositories;
using Teste.Impl.Context;
using Teste.Impl.Repository;
using Impl.Context;
using Teste.Domain.Repositories;

namespace Teste.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors();

            Services(services);

            services.AddDbContext<DataContext>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<ICityRepository, CityRepository>();

            services.AddScoped<IAddressRepository, AddressRepository>();

            services.AddScoped<IAuthorBookRepository, AuthorBookRepository>();

            services.AddSingleton<IDbInitializer, DbInitializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseMvc();

            DbInitialize(app.ApplicationServices.GetService<IDbInitializer>());
        }

        private void Services(IServiceCollection services)
        {
            services.AddTransient<ContractPersonApp>();
            services.AddTransient<ContractUserApp>();
            services.AddTransient<ContractBookApp>();
            services.AddTransient<ContractProfessionApp>();
            services.AddTransient<ContractTrainingAreaApp>();
            services.AddTransient<ContractAuthorBookApp>();
            services.AddTransient<ContractStateApp>();
            services.AddTransient<ContractCityApp>();
            services.AddTransient<ContractAddressApp>();
        }

        public void DbInitialize(IDbInitializer dbInitializer)
        {
            dbInitializer.Initialize();
        }
    }
}
