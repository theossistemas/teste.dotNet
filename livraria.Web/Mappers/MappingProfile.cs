using AutoMapper;
using livraria.Domain.entities;
using livraria.Web.ViewModel;

namespace livraria.Web.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioLogin>().ReverseMap();
            CreateMap<Livro, LivroResponseVM>().ReverseMap();
        }
    }
}
