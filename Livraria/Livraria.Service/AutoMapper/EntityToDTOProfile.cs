using AutoMapper;
using Livraria.Domain.Entity;
using Livraria.Service.DTOs;

namespace Livraria.Service.AutoMapper
{
    internal class EntityToDTOProfile : Profile
    {
        public EntityToDTOProfile()
        {
            CreateMap<Livro, LivroDTO>();
            CreateMap<Autor, AutorDTO>();
            CreateMap<Editora, EditoraDTO>();
        }
    }
}