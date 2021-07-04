using AutoMapper;
using Livraria.Domain.Dto.Administracao;
using Livraria.Domain.Entities.Administracao;
using Livraria.Util.ExtensionMethods;

namespace Livraria.Api.AutoMapper
{
    public class UsuarioProfileMap : Profile
    {
        public UsuarioProfileMap()
        {
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(dest => dest.Role, opts => opts.MapFrom(src => src.Role.ToEnumString()));

            CreateMap<UsuarioDto, Usuario>()
                .ForMember(dest => dest.Senha, opts => opts.MapFrom(src => src.Senha.HashString()))
                .ForMember(dest => dest.Role, opts => opts.MapFrom(src => src.Role.ToEnum<TipoUsuario>()));
        }
    }
}
