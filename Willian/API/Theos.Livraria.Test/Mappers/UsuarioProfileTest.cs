using AutoMapper;
using Theos.Livraria.Application.Services;
using Xunit;

namespace Theos.Livraria.Test.Mappers
{
    public class UsuarioProfileTest
    {
        private readonly MapperConfiguration config;

        public UsuarioProfileTest()
        {
            config = new MapperConfiguration(cfg => cfg.AddProfile<UsuarioProfile>());
        }

        [Fact]
        public void Shopline_ConfigMapper()
        {
            config.AssertConfigurationIsValid();
        }
    }
}