using AutoMapper;
using Theos.Library.Domain.Base;

namespace Theos.Library.Core.Data.Mapper
{
    public class MapperDomain2Domain : Profile
    {
        public MapperDomain2Domain()
        {
            CreateMap<BaseRelationShip<BaseKey>, BaseRelationShip<BaseKey>>()
                .ForMember(to => to.Id, map => map.Ignore())
                .ForMember(to => to.Date, map => map.Ignore())
                .ForMember(to => to.UserId, map => map.Ignore())
                .ForMember(to => to.Key, map => map.Ignore())
                .ForMember(to => to.KeyId, map => map.Ignore())
                ;
        }
    }
}
