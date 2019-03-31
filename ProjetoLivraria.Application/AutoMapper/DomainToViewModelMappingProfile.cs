using AutoMapper;
using ProjetoLivraria.Application.ViewModels;
using ProjetoLivraria.Domain.Entities;

namespace ProjetoLivraria.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Livro, LivroViewModel>();
        }
    }
}
