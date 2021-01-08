using AutoMapper;
using BooksApi.Dto;
using BooksApi.Models;

namespace BooksApi.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Livro, LivroDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
           
        }
    }
}