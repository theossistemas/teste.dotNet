using AutoMapper;
using Theos.Livraria.Domain.Entity; 
using Theos.Livraria.Domain.Model.Livro;

namespace Theos.Livraria.Application.Services
{
    public class LivroProfile : Profile
    {
        public LivroProfile()
        {
            CreateMap<Livro, ResponseLivro>();
              

            CreateMap<RequestLivro, Livro>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo))
                 .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
                 .ForMember(dest => dest.Autor, opt => opt.MapFrom(src => src.Autor))
                 .ForMember(dest => dest.Paginas, opt => opt.MapFrom(src => src.Paginas))
                 .ForMember(dest => dest.Edicao, opt => opt.MapFrom(src => src.Edicao))
                 .ForMember(dest => dest.Idioma, opt => opt.MapFrom(src => src.Idioma))
                 .ForMember(dest => dest.Editora, opt => opt.MapFrom(src => src.Editora))
                 .ForMember(dest => dest.DataPublicacao, opt => opt.MapFrom(src => src.DataPublicacao))
                 .ForMember(dest => dest.Estoque, opt => opt.MapFrom(src => src.Estoque))
                 .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Ativo))
                 .ForMember(dest => dest.IdUsuario, opt => opt.MapFrom(src => src.IdUsuario))
                 .ForMember(dest => dest.Imagem, opt => opt.MapFrom(src => src.Imagem))
                 .ForMember(dest => dest.DataCadastro, opt => opt.Ignore())
                 .ForMember(dest => dest.DataAlteracao, opt => opt.Ignore());
        }
    }
}
