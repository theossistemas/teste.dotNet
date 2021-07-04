using Livraria.Api.AutoMapper;
using Livraria.Infra.Data.Context;
using Livraria.Infra.Data.Interfaces.Repositories.Administracao;
using Livraria.Infra.Data.Interfaces.Repositories.Cadastros;
using Livraria.Infra.Data.Repositories.Administracao;
using Livraria.Infra.Data.Repositories.Cadastros;
using Livraria.Services.Administracao;
using Livraria.Services.Cadastros;
using Livraria.Services.Interfaces.Administracao;
using Livraria.Services.Interfaces.Cadastros;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace Livraria.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer().AddDbContext<LivrariaDataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddControllers();

            ConfigureAuthorization(services);
            ConfigureRepositories(services);
            ConfigureBusinessServices(services);

            services.AddAutoMapper(Assembly.Load(Assembly.GetAssembly(typeof(UsuarioProfileMap)).FullName));
            services.AddAutoMapper(Assembly.Load(Assembly.GetAssembly(typeof(LivroProfileMap)).FullName));
            services.AddAutoMapper(Assembly.Load(Assembly.GetAssembly(typeof(LogProfileMap)).FullName));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Livraria API", Version = "v1" });
            });

        }

        public void ConfigureBusinessServices(IServiceCollection services)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IConfigurationManagerService, ConfigurationManagerService>();
            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<ILogService, LogService>();
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ILivroRepositorio, LivroRepositorio>();
            services.AddScoped<ILogRepositorio, LogRepositorio>();
        }

        private void ConfigureAuthorization(IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Configuration["SecurityKey"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["IssuerKey"],
                    ValidAudience = Configuration["AudienceKey"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Livraria v1");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
