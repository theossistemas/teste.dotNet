using Bogus;
using Livraria.Common.Utils;
using Livraria.Domain.Entidades;
using Livraria.Tests.Comum;
using Xunit;

namespace Livraria.Tests.Livros
{
    public class LivroTeste
    {
        private readonly Faker _faker;

        public LivroTeste()
        {
            _faker = FakerBuilder.Novo().Build();
        }

        [Fact]
        public void DeveCriarUmLivro()
        {
            //arrange
            var livroEsperado = LivroBuilder.Novo().Build();

            //act
            var livro = new Livro(
                livroEsperado.Titulo,
                livroEsperado.AnoDePublicacao, 
                livroEsperado.Edicao,
                livroEsperado.AutorId);

            //assert
            Assert.Equal(livroEsperado.Titulo, livro.Titulo);
            Assert.Equal(livroEsperado.AnoDePublicacao, livro.AnoDePublicacao);
            Assert.Equal(livroEsperado.Edicao, livro.Edicao);
            Assert.Equal(livroEsperado.AutorId, livro.AutorId);
        }

        [Fact]
        public void NaoDeveCriarUmLivroComTituloMaiorQueLimite()
        {
            //arrange
            var tituloInvalido = _faker.Lorem.Paragraphs();
            var livro = LivroBuilder.Novo().ComTitulo(tituloInvalido).Build();

            //act
            var resultado = livro.Validar();

            //assert
            Assert.False(resultado);
        }

        [Fact]
        public void NaoDeveCriarUmLivroComAnoDePublicacaoInvalido()
        {
            //arrange
            var anoDePublicacaoInvalido = Constantes.Zero;
            var livro = LivroBuilder.Novo().ComAnoDePublicacao(anoDePublicacaoInvalido).Build();

            //act
            var resultado = livro.Validar();

            //assert
            Assert.False(resultado);
        }

        [Fact]
        public void NaoDeveCriarUmLivroComEdicaoInvalido()
        {
            //arrange
            var edicaoInvalida = Constantes.Zero;
            var livro = LivroBuilder.Novo().ComEdicao(edicaoInvalida).Build();

            //act
            var resultado = livro.Validar();

            //assert
            Assert.False(resultado);
        }
    }
}
