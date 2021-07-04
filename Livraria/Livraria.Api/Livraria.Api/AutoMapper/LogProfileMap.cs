using AutoMapper;
using Livraria.Domain.Dto.Administracao;
using Livraria.Domain.Entities.Administracao;

namespace Livraria.Api.AutoMapper
{
    public class LogProfileMap : Profile
    {
        public LogProfileMap()
        {
            CreateMap<Log, LogDto>()
                .ForMember(dest => dest.Exception, opts => opts.MapFrom(src => src.Exception))
                .ForMember(dest => dest.Level, opts => opts.MapFrom(src => src.Level))
                .ForMember(dest => dest.Message, opts => opts.MapFrom(src => src.Message))
                .ForMember(dest => dest.MessageTemplate, opts => opts.MapFrom(src => src.MessageTemplate))
                .ForMember(dest => dest.Properties, opts => opts.MapFrom(src => src.Properties))
                .ForMember(dest => dest.TimeStamp, opts => opts.MapFrom(src => src.TimeStamp));

            CreateMap<LogDto, Log>(MemberList.None)
                .ForMember(dest => dest.Exception, opts => opts.MapFrom(src => src.Exception))
                .ForMember(dest => dest.Level, opts => opts.MapFrom(src => src.Level))
                .ForMember(dest => dest.Message, opts => opts.MapFrom(src => src.Message))
                .ForMember(dest => dest.MessageTemplate, opts => opts.MapFrom(src => src.MessageTemplate))
                .ForMember(dest => dest.Properties, opts => opts.MapFrom(src => src.Properties))
                .ForMember(dest => dest.TimeStamp, opts => opts.MapFrom(src => src.TimeStamp));
        }
    }
}
