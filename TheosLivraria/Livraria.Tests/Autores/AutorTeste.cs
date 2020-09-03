using Bogus;
using Livraria.Domain.Entidades;
using Livraria.Tests.Comum;
using Xunit;

namespace Livraria.Tests.Autores
{
    public class AutorTeste
    {
        private readonly Faker _faker;

        public AutorTeste()
        {
            _faker = FakerBuilder.Novo().Build();
        }

        [Fact]
        public void DeveCriarUmAutor()
        {
            //arrange
            var autorEsperado = AutorBuilder.Novo().Build();

            //act
            var autor = new Autor(autorEsperado.Nome);

            //assert

            Assert.Equal(autorEsperado.Nome, autor.Nome);
        }

        [Fact]
        public void NaoDeveCriarUmAutorComNomeMaiorQueLimite()
        {
            //arrange
            var nomeInvalido = _faker.Lorem.Paragraph();
            var autor = AutorBuilder.Novo().ComNome(nomeInvalido).Build();

            //act
            var resultado = autor.Validar();

            //assert

            Assert.False(resultado);
        }
    }
}
