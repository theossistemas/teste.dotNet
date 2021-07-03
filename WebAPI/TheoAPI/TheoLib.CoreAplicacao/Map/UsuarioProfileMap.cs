using AutoMapper;
using TheoLib.Dominio.Entidade;
using TheoLib.Dominio.Modelo.UsuarioModelo;

namespace TheoLib.CoreAplicacao.Map
{
    public class UsuarioProfileMap : Profile
    {
        public UsuarioProfileMap()
        {
            CreateMap<Usuario, RespostaUsuario>();

            CreateMap<RequisicaoUsuario, Usuario>()
                .ForMember(dUsu => dUsu.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dUsu => dUsu.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dUsu => dUsu.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dUsu => dUsu.Senha, opt => opt.MapFrom(src => src.Senha));
        }
    }
}
