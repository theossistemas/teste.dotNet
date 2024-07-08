using AutoMapper;
using Gerenciador.Livraria.Core.Entities;
using Gerenciador.Livraria.Core.Entities.Livraria;
using Gerenciador.Livraria.DTOs.DTOs.GoogleBooks;
using Gerenciador.Livraria.DTOs.DTOs.Livros;

namespace Gerenciador.Livraria.API.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<LivroEntity, LivroDTO>()
                .ForMember(x => x.AutorNome, y => y.MapFrom(z => z.Autor.Nome))
                .ForMember(x => x.CategoriaNome, y => y.MapFrom(z => z.Categoria.Nome));

            CreateMap<AutorEntity, AutorDTO>();
            CreateMap<CategoriaEntity, CategoriaDTO>();
            CreateMap<LivroDTO, LivroEntity>();
            CreateMap<AutorDTO, AutorEntity>();
            CreateMap<CategoriaDTO, CategoriaEntity>();
        }
    }
}
