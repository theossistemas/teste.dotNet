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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "APITheos-Teste", Version = "v1" });
            });

            services.AddAuthorization(options =>
            {
                Permissao[] permissoes = (Permissao[])Enum.GetValues(typeof(Permissao));

                foreach (Permissao permissao in permissoes)
                {
                    options.AddPolicy(permissao.ToString(), policy => policy.Requirements.Add(new PermissaoAcessoRequirement(permissao)));
                }
            });

            services.AddScoped<IAuthorizationHandler, PermissaoAcessoHandler>();

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "APITheos-Teste");
            });

            app.UseAuthentication();
        }
    }
}
