using LivrariaTheos.Estoque.Application.Services;
using LivrariaTheos.Estoque.Data;
using LivrariaTheos.Estoque.Data.Repository;
using LivrariaTheos.Estoque.Domain.Autores.Interfaces;
using LivrariaTheos.Estoque.Domain.Generos;
using LivrariaTheos.Estoque.Domain.Livros;
using LivrariaTheos.Estoque.Domain.Livros.Interfaces;
using LivrariaTheos.Estoque.Domain.Logs;
using LivrariaTheos.Estoque.Domain.Logs.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LivrariaTheos.WebApp.Api.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<ILivroAppService, LivroAppService>();
            services.AddScoped<ArmazenadorDeLivro>();

            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped<IAutorAppService, AutorAppService>();

            services.AddScoped<IGeneroRepository, GeneroRepository>();
            services.AddScoped<IGeneroAppService, GeneroAppService>();

            services.AddScoped<ILogAplicacaoRepository, LogAplicacaoRepository>();       
            services.AddScoped<ArmazenadorDeLogAplicacao>();

            services.AddScoped<EstoqueContext>();
        }
    }
}
