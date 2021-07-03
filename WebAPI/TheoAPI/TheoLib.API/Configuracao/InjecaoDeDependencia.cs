using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TheoLib.CoreAplicacao.Map;
using TheoLib.CoreAplicacao.Servicos;
using TheoLib.Dados.Repositorio;
using TheoLib.Dominio.Contratos.Repositorio;
using TheoLib.Dominio.Contratos.Servicos;

namespace TheoLib.API.Configuracao
{
    public static class InjecaoDeDependencia
    {

        public static IServiceCollection ConfiguracaoDaInjecaoDeDependencia(this IServiceCollection services)
        {
            //Serviços
            services.AddTransient<ILivroServico, LivroServico>();
            services.AddTransient<IUsuarioServico, UsuarioServico>();

            //Repositorios
            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddTransient<ILivroRepositorio, LivroRepositorio>();

            //Profiles
            services.AddAutoMapper(typeof(LivroProfileMap));
            services.AddAutoMapper(typeof(UsuarioProfileMap));

            return services;
        }
    }
}
