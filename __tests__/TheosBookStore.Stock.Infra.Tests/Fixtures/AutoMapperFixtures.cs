using AutoMapper;
using TheosBookStore.Stock.Infra.Mappers.Profiles;

namespace TheosBookStore.Stock.Infra.Tests.Fixtures
{
    public class AutoMapperFixture
    {
        public IMapper Mapper { get; private set; }

        public AutoMapperFixture()
        {
            var _mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AuthorProfile>();
            });
            Mapper = _mapperConfig.CreateMapper();
        }
    }
}
