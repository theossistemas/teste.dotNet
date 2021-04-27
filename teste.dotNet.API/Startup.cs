using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using teste.dotNet.API.Data;
using teste.dotNet.API.Repository;
using teste.dotNet.API.Repository.Impl;
using teste.dotNet.API.Services;
using teste.dotNet.API.Services.Impl;

namespace teste.dotNet.API
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "teste.dotNet.API", Version = "v1" });
            });

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection")
            );
            services.AddTransient<BooksService, BooksServiceImpl>();
            services.AddTransient<WritersService, WritersServiceImpl>();
            services.AddTransient<BooksRepository, BooksRepositoryImpl>();
            services.AddTransient<WritersRepository, WritersRepositoryImpl>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "teste.dotNet.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
