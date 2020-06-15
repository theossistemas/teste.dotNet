using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MMM.Library.Application.AutoMapper;
using MMM.Library.Infra.CrossCutting.Identity.ApiConfiguration;
using MMM.Library.Infra.CrossCutting.Identity.UsersSedeer;
using MMM.Library.Infra.CrossCutting.IoC;
using MMM.Library.Services.AspNetWebApi.Configurations;
using System;
using System.Linq;

namespace MMM.Library.Services.AspNetWebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            if (env.IsDevelopment())
            {
                // Adicionar UserSecrets 
                // builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcSetup();

            services.AddDatabaseSetup(Configuration);

            services.AddIdentitySetup(Configuration);

            services.AddAuthSetup(Configuration);

            services.AddSwaggerSetup();

            services.AddLogSetup(Configuration);

            services.AddAutoMapper(typeof(AutoMapperSetup));

            services.AddMediatR(typeof(Startup));

            // Dependencias 
            services.ResolveDependencies();

            // Api Versioning
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            // Gzip Compression
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
            });

            // Caching all Api
            // services.AddResponseCaching();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicyDevelopment",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                              IServiceProvider serviceProvider)

        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("CorsPolicyDevelopment");
            }
            else
            {
                app.UseExceptionHandler("/error");
                // app.UseCors("CorsPolicyDevelopment");
            }

            // ->
            app.UseSwaggerSetup();
            app.UseLogSetup(Configuration);
            // -<

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseGlobalizationSetup();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Add new Dummy Users: 
            // user1 name: Admin, user1 password: Admin@123
            // user2 name: User, user2 password: User@123
            DummyUsersSedeer.AddUsersWithRoles(serviceProvider).Wait();
        }
    }
}
