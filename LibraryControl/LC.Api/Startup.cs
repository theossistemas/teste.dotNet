using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using LC.Persistence;
using LC.Persistence.Configuration;
using LC.Infrastruture.Repositories.Contracts;
using LC.Infrastruture.Repositories.Implementation;
using LC.Aplication.Book;
using LC.Commons;
using LC.Api.Middleware;

namespace LB.Api
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

            // Add service and create Policy with options
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddDbContext<DataBaseContext>(ConfigureDataBaseContext);

            services.AddTransient<DbInitializer>();
            
            services.AddScoped<IBookRepositoy, BookRepositoy>();

            services.AddSingleton<GenerateSlug>();
            services.AddScoped<ApplicationBook>();

            services.AddMvc( options => { options.ReturnHttpNotAcceptable = true; } )
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opt => {  opt.SerializerSettings.ContractResolver = new DefaultContractResolver()
                    {  NamingStrategy = new SnakeCaseNamingStrategy() };
                });

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);

            services.AddResponseCompression(options => {
                options.Providers.Add<GzipCompressionProvider>();
            });

          
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "LibraryControl API",
                    Description = "Control Library",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Elvisley Souza",
                        Email = string.Empty
                    }
                });
            });

        }

        void ConfigureDataBaseContext(DbContextOptionsBuilder options)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
            options.UseSqlServer(connectionString);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseCors("CorsPolicy");

            app.ApplicationServices.GetService<DbInitializer>().Initialize();

            app.UseSwagger();

          //  app.UseStatusCodePages();

           app.UseResponseCompression();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LibraryControl V1");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiddleware<ExceptionHandler>();

            app.UseHttpsRedirection();
            app.UseMvc();
           

        }
    }
}
