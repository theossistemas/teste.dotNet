using AutoMapper;
using Livraria.Domain.Dto.Cadastros;
using Livraria.Domain.Entities.Cadastros;

namespace Livraria.Api.AutoMapper
{
    public class LivroProfileMap : Profile
    {
        public LivroProfileMap()
        {
            CreateMap<Livro, LivroDto>()
                .ForMember(dest => dest.Autor, opts => opts.MapFrom(src => src.Autor))
                .ForMember(dest => dest.Titulo, opts => opts.MapFrom(src => src.Titulo))
                .ForMember(dest => dest.Codigo, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Genero, opts => opts.MapFrom(src => src.GeneroId.ToString()));

            CreateMap<LivroDto, Livro>(MemberList.None)
                .ForMember(dest => dest.Autor, opts => opts.MapFrom(src => src.Autor))
                .ForMember(dest => dest.Titulo, opts => opts.MapFrom(src => src.Titulo))
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.GeneroId, opts => opts.MapFrom(src => src.Genero))
                .ForMember(dest => dest.Genero, opts => opts.Ignore());

        }
    }
}
