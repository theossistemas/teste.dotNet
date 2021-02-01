using AutoMapper;
using Theos.Livraria.Application.Services;
using Xunit;

namespace Theos.Livraria.Test.Mappers
{
    public class LivroProfileTest
    {
        private readonly MapperConfiguration config;

        public LivroProfileTest()
        {
            config = new MapperConfiguration(cfg => cfg.AddProfile<LivroProfile>());
        }

        [Fact]
        public void Shopline_ConfigMapper()
        {
            config.AssertConfigurationIsValid();
        }
    }
}