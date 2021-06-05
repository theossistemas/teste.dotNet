using AutoMapper;
using TesteDotNet.Api.ViewModel;
using TesteDotNet.Business.Models;


namespace TesteDotNet.Api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Livro, LivroViewModel>().ReverseMap();
        }
    }
}

