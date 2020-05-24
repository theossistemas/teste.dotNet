using Business.Services;
using Business.Services.Interface;
using Library.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repository;
using Persistence.Repository.Interface;
using Persistence.Repository.Interfaces;
using Swashbuckle.AspNetCore.Swagger;

namespace Library
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Api", Version = "v1" });
            });

            services.AddDbContext<DataContext>(options =>
                       options.UseSqlServer(Configuration.GetConnectionString("dbConnection"),
                       opt => opt.MigrationsAssembly("Library")));

            var config = new AutoMapper.MapperConfiguration(x =>
            {
                x.AddProfile(new AutoMapperProfile());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ILivrosServices, LivrosServices>();
            services.AddScoped<ILivroRepository, LivroRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<DataContext>().Database.Migrate();
                    serviceScope.ServiceProvider.GetService<DataContext>().EnsureSeedData();
                }
            }

            app.UseMvc();
        }
    }
}
