using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TesteDotNet.Api.ViewModel;
using TesteDotNet.Business.Interfaces;
using TesteDotNet.Business.Models;
using TesteDotNet.Data.Context;
using TesteDotNet.Data.Repository;
using AutoMapper;
using Microsoft.OpenApi.Models;
using Swashbuckle.Swagger;
using TesteDotNet.Api.Configuration;

namespace TesteDotNet.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

  
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            });
            services.AddIdentityConfiguration(Configuration);
            services.AddControllers();
            services.AddScoped<DataDbContext>();
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TesteDotNet"
                });
            });
        }

  
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Teste Dot Net");
            });
            

        }
    }
}
