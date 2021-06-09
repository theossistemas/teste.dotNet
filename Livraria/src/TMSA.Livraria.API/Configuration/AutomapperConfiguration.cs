using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TMSA.Livraria.API.ViewModels;
using TMSA.Livraria.Domain.Models;

namespace TMSA.Livraria.API.Configuration
{
    public class AutomapperConfiguration : Profile
    {
        public AutomapperConfiguration()
        {
            CreateMap<Livro, LivroViewModel>().ReverseMap();
        }
    }

    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfig(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new AutomapperConfiguration());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
