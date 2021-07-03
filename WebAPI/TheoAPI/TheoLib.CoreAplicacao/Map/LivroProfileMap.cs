using AutoMapper;
using TheoLib.Dominio.Entidade;
using TheoLib.Dominio.Modelo.LivroModelo;

namespace TheoLib.CoreAplicacao.Map
{
    public class LivroProfileMap : Profile
    {
        public LivroProfileMap()
        {
            CreateMap<Livro, RespostaLivro>();

            CreateMap<RequisicaoLivro, Livro>()
                .ForMember(dLiv => dLiv.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dLiv => dLiv.Titulo, opt => opt.MapFrom(src => src.Titulo))
                .ForMember(dLiv => dLiv.Descricao, opt => opt.MapFrom(src => src.Descricao))
                .ForMember(dLiv => dLiv.Autor, opt => opt.MapFrom(src => src.Autor))
                .ForMember(dLiv => dLiv.DataPublicacao, opt => opt.MapFrom(src => src.DataPublicacao))
                .ForMember(dLiv => dLiv.Estoque, opt => opt.MapFrom(src => src.Estoque))
                .ForMember(dLiv => dLiv.Ativo, opt => opt.MapFrom(src => src.Ativo))
                .ForMember(dLiv => dLiv.IdUsuario, opt => opt.MapFrom(src => src.IdUsuario));
        }
    }
}
