using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Livraria.Domain.Entities;
using Livraria.Domain.Interfaces;
using Livraria.Service.Services;

using Livraria.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using livraria_api.Models;
using Livraria.Infra.Data.Repository;
using livraria_api.StartupExtensions;
using Livraria.Domain.Security.Interfaces;
using Livraria.Infra.Data.Security.Repository;
using Livraria.Service.Security.Services;
using Livraria.Domain.Security.Entities;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace livraria_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddCors();

            services.AddControllers();

            services.AddCustomizedDatabase(Configuration, _env);
            
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISecurityUserService, UserSecurityService>();

            services.AddSingleton(new MapperConfiguration(config =>
            {
                config.CreateMap<LivroModels, Livro>();
                config.CreateMap<Livro, LivroModels>();
                config.CreateMap<User, User>();
                config.CreateMap<UsuarioModels, User>();
                config.CreateMap<User, UsuarioModels>();
            }).CreateMapper());

            services.AddCustomizedSwagger(_env);

            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            services.AddAuthentication(el =>
            {
                el.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                el.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(el =>
            {
                el.RequireHttpsMetadata = false;
                el.SaveToken = true;
                el.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseCustomizedSwagger(_env);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // app.UseCors("CorsPolicy");
            
        }
    }
}
