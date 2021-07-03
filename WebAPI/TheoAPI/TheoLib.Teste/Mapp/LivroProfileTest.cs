using AutoMapper;
using TheoLib.CoreAplicacao.Map;
using Xunit;

namespace TheoLib.Teste.Mapp
{
    public class LivroProfileTest
    {
        private readonly MapperConfiguration config;

        public LivroProfileTest()
        {
            config = new MapperConfiguration(cfg => cfg.AddProfile<LivroProfileMap>());
        }

        [Fact]
        public void Shopline_ConfigMapper()
        {
            config.AssertConfigurationIsValid();
        }

    }
}
