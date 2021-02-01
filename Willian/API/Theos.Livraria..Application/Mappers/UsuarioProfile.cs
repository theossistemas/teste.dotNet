using AutoMapper;
using Theos.Livraria.Domain.Entity; 
using Theos.Livraria.Domain.Model.Usuario;

namespace Theos.Livraria.Application.Services
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, ResponseUsuario>();

            CreateMap<RequestUsuario, Usuario>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                 .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                 .ForMember(dest => dest.Senha, opt => opt.MapFrom(src => src.Senha))
                 .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Ativo))
                 .ForMember(dest => dest.PrimeiroAcesso, opt => opt.Ignore())
                 .ForMember(dest => dest.DataCadastro, opt => opt.Ignore())
                 .ForMember(dest => dest.DataAlteracao, opt => opt.Ignore());
        }
    }
}
