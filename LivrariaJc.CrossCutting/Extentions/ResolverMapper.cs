using AutoMapper;
using LivrariaJc.Data.Mapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LivrariaJc.CrossCutting.Extetions
{
    public static class ResolverMapper
    {
        public static IServiceCollection ResolveMapperConfigration(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new LivroMapper());
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
