using AutoMapper;
using TheoLib.CoreAplicacao.Map;
using Xunit;

namespace TheoLib.Teste.Mapp
{
    public class UsuarioProfileTest
    {

        private readonly MapperConfiguration config;

        public UsuarioProfileTest()
        {
            config = new MapperConfiguration(cfg => cfg.AddProfile<UsuarioProfileMap>());
        }

        [Fact]
        public void Shopline_ConfigMapper()
        {
            config.AssertConfigurationIsValid();
        }

    }
}
