using AutoMapper;
using LivrariaTheos.Estoque.Domain.Dtos;
using LivrariaTheos.Estoque.Domain.Autores;
using LivrariaTheos.Estoque.Domain.Generos;
using LivrariaTheos.Estoque.Domain.Livros;
using LivrariaTheos.Core;

namespace LivrariaTheos.Estoque.Application.AutoMapper
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<LivroDto, Livro>()
                 .ConstructUsing(p =>
                    new Livro(p.AutorId, p.GeneroId, p.Nome,
                        p.Sinopse, p.QuantidadePaginas, 
                        p.Ativo))               
                .ForMember(dto => dto.CaminhoCapa, o => o.MapFrom(p => Resources.CaminhoCapas))
                .ForMember(dto => dto.Autor, o => o.AllowNull())
                .ForMember(dto => dto.Genero, o => o.AllowNull());
               

            CreateMap<GeneroDto, Genero>()
                .ConstructUsing(p =>
                    new Genero(p.Nome, p.Ativo));
          
            CreateMap<AutorDto, Autor>()
                .ConstructUsing(p =>
                    new Autor(p.Nome, p.Nacionalidade, p.InformacoesRelevantes,
                        p.Ativo));
        }
    }
}