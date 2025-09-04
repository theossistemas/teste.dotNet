using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheosLivraria.Core.Sergurancao;
using TheosLivraria.Domain.Interface;
using TheosLivraria.Infra.Configuracao;
using TheosLivraria.Infra.Repository.Base;
using TheosLivraria.Infra.Repository.Livros;
using TheosLivraria.Infra.Repository.Logs;
using TheosLivraria.Infra.Repository.Usuarios;

namespace TheosLivraria.Shared.Ioc
{
    public static class DependencyContainer
    {
        public static void RegistroDI(this IServiceCollection services, IConfiguration configuration, string connectionName = "TheosDefaultConnection")
        {
            var connectionString = GetConnectionString(configuration, connectionName);
            InjetarDbContext(services, connectionString);
            InjetarRepositorios(services);
            InjetarServicos(services);
        }

        public static string GetConnectionString(IConfiguration configuration, string nome)
        {
            var connectionString = configuration.GetConnectionString(nome);

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException($"Connection string '{nome}' não encontrada no arquivo de configuração.");

            return connectionString;
        }

        public static void InjetarDbContext(IServiceCollection services, string connectionString, int poolSize = 128)
        {
            services.AddDbContextPool<TheosApplicationDbContext>(options =>
                options.UseSqlServer(connectionString), poolSize);
        }

        public static void InjetarServicos(IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
        }

        public static void InjetarRepositorios(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
        }
    }
}