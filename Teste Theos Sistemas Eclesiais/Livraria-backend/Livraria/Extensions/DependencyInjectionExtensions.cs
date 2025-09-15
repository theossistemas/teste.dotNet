using Livraria.Api.Authentication;
using Livraria.Aplicacao.Interfaces;
using Livraria.Application.Interfaces;
using Livraria.Application.Services;
using Livraria.Domain.Interfaces;
using Livraria.Infrastructure.Repositories;

namespace Livraria.API.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Injeção de Dependência
            services.AddScoped<IAutenticacaoJWT, AutenticacaoJWT>();
            services.AddTransient<ILivroRepository, LivroRepository>();
            services.AddTransient<ILivroService, LivroService>();
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<ICategoriaService, CategoriaService>();

            return services;
        }
    }
}
