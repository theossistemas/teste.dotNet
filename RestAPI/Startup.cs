using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Enumerators;
using System;
using Services.Usuarios;
using Services.Livros;
using Utils.Connection;
using Services.AtualizacaoSistema;
using Services.Acesso;
using Microsoft.AspNetCore.Authorization;
using Repositories.Usuarios;
using Repositories.Livros;
using System.Globalization;
using Microsoft.AspNetCore.Authentication;

namespace RestAPI
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

            services.AddLogging();

            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API-Theos-Teste", Version = "v1" });
            });

            services.AddAuthentication("BasicAuthentication")
               .AddScheme<AuthenticationSchemeOptions, PermissaoAcessoHandler>("BasicAuthentication", null);

            services.AddSingleton<IUsuarioRepository, UsuarioRepository>();
            services.AddSingleton<ILivroRepository, LivroRepository>();

            services.AddSingleton<IUsuarioService, UsuarioService>();
            services.AddSingleton<ILivroService, LivroService>();

            SqlServerHelper.Initializer(Configuration.GetConnectionString("default"));

            AtualizacaoService.Iniciar();
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

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API-Theos-Teste");
            });
        }
    }
}
