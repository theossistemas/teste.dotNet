using AutoMapper;

namespace Livraria.Service.AutoMapper
{
    public class AutoMapperConfigure
    {
        public static MapperConfiguration Configure()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EntityToDTOProfile());
                cfg.AddProfile(new DTOToEntityProfile());
            });
        }
    }
}
