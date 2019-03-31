using AutoMapper;
using ProjetoLivraria.Application.ViewModels;
using ProjetoLivraria.Domain.Entities;

namespace ProjetoLivraria.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<LivroViewModel, Livro>();
        }
    }
}
