using AutoMapper;
using Library.Dto;
using Library.ViewModels;
using Persistence.Entity;

namespace Library.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LivroDto, Livro>();
            CreateMap<Livro, LivroViewModel>();
        }
    }
}
