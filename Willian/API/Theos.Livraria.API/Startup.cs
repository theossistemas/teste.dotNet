using AutoMapper;
using FluentValidation.AspNetCore; 
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Theos.Livraria.Api.Configuration;
using System; 

namespace CrudNetCore.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt =>
            {
                opt.AllowEmptyInputInBodyModelBinding = true;
                opt.EnableEndpointRouting = false;
            })
            .ConfigureApiBehaviorOptions(opt => opt.SuppressMapClientErrors = true)
            .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());
              
            services.AddOptions();

            services.AddCors(setup => setup.AddPolicy("AllowAll", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

            // automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // configurar autenticação
            services.ConfigurationAuthentication(Configuration);

            // configurar dependencias
            services.ConfigurationDependencyInjection();

            // swagger
            services.ConfigurationSwagger();

            // Health Check     
            services.ConfigurationHealthChecks(Configuration);

            // serilog
            services.ConfigurationLog(Configuration);

            services.AddHttpContextAccessor(); 
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseConfigurationSwagger();
            app.UseMvcWithDefaultRoute();
        }
    }
}