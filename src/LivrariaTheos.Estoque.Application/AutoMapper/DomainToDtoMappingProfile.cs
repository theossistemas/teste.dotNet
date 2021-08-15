using AutoMapper;
using LivrariaTheos.Estoque.Domain.Autores;
using LivrariaTheos.Estoque.Domain.Dtos;
using LivrariaTheos.Estoque.Domain.Generos;
using LivrariaTheos.Estoque.Domain.Livros;
using LivrariaTheos.Estoque.Domain.Livros.Dto;

namespace LivrariaTheos.Estoque.Application.AutoMapper
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Livro, LivroDto>();
            CreateMap<Livro, LivroDtoRetorno>();
            CreateMap<Genero, GeneroDto>();
            CreateMap<Autor, AutorDto>();
        }
    }
}