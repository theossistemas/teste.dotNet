using Bogus;
using Livraria.Common.Handler;
using Livraria.Common.Implementation;
using Livraria.Common.Utils;
using Livraria.Domain.Dto;
using Livraria.Domain.Entidades;
using Livraria.Domain.Serviços.Validadores;
using Livraria.Tests.Comum;
using System.Linq;
using Xunit;

namespace Livraria.Tests.Livros
{
    public class ValidadorDeLivrosTeste
    {
        private NotifiyHandler _notifiyHandler;
        private Notify _notify;
        private ValidadorDeLivro _validadorDeLivro;
        private readonly Faker _faker;

        public ValidadorDeLivrosTeste()
        {
            _notifiyHandler = new NotifiyHandler();
            _notify = new Notify(_notifiyHandler);
            _validadorDeLivro = new ValidadorDeLivro(_notify);
            _faker = FakerBuilder.Novo().Build();
        }

        [Fact]
        public void DeveNotificarLivroDtoInvalido()
        {
            //arrange
            var mensagemEsperada = Resources.LivroNulo;
            LivroDto dto = null;

            //act
            _validadorDeLivro.Validar(dto);
            var resultado = _notify.IsValid();
            var notificacaoExistente = _notifiyHandler.GetNotifications().Any(x => x.Value.Equals(mensagemEsperada));

            //asset
            Assert.False(resultado);
            Assert.True(notificacaoExistente);
        }

        [Fact]
        public void DeveNotificarLivroDtoComAutorInvalido()
        {
            //arrange
            var mensagemEsperada = Resources.LivroComAutorNulo;
            var dto = ObterDtoComAutorInvalido();

            //act
            _validadorDeLivro.Validar(dto);
            var resultado = _notify.IsValid();
            var notificacaoExistente = _notifiyHandler.GetNotifications().Any(x => x.Value.Equals(mensagemEsperada));

            //asset
            Assert.False(resultado);
            Assert.True(notificacaoExistente);
        }

        [Fact]
        public void NaoDeveNotificarLivroDtoComAutorValido()
        {
            //arrange
            var dto = ObterDtoComAutorValido();

            //act
            _validadorDeLivro.Validar(dto);
            var resultado = _notify.IsValid();

            //asset
            Assert.True(resultado);
        }

        [Fact]
        public void DeveNotificarSeLivroNaoEncontrado()
        {
            //arrange
            var mensagemEsperada = Resources.LivroNaoEncontrado;
            Livro livro = null;

            //act
            _validadorDeLivro.ValidarLivroEncontrado(livro);
            var resultado = _notify.IsValid();
            var notificacaoExistente = _notifiyHandler.GetNotifications().Any(x => x.Value.Equals(mensagemEsperada));

            //asset
            Assert.False(resultado);
            Assert.True(notificacaoExistente);
        }

        [Fact]
        public void NaoDeveNotificarSeLivroNaoEncontrado()
        {
            //arrange
            var livro = LivroBuilder.Novo().Build();

            //act
            _validadorDeLivro.ValidarLivroEncontrado(livro);
            var resultado = _notify.IsValid();

            //asset
            Assert.True(resultado);
        }

        [Fact]
        public void DeveNotificarSeLivroExiste()
        {
            //arrange
            var mensagemEsperada = Resources.LivroJaExiste;
            var livro = LivroBuilder.Novo().Build();

            //act
            _validadorDeLivro.ValidarSeLivroExiste(livro);
            var resultado = _notify.IsValid();
            var notificacaoExistente = _notifiyHandler.GetNotifications().Any(x => x.Value.Equals(mensagemEsperada));

            //asset
            Assert.False(resultado);
            Assert.True(notificacaoExistente);
        }

        [Fact]
        public void NaoDeveNotificarSeLivroExiste()
        {
            //arrange
            var mensagemEsperada = Resources.LivroJaExiste;
            Livro livro = null;

            //act
            _validadorDeLivro.ValidarSeLivroExiste(livro);
            var resultado = _notify.IsValid();

            //asset
            Assert.True(resultado);
        }

        private LivroDto ObterDtoComAutorInvalido()
        {
            return new LivroDto()
            {
                AnoDePublicacao = _faker.Random.Int(1990, 2020),
                Autor = null,
                AutorId = Constantes.Zero,
                Edicao = _faker.Random.Int(1, 10),
                Id = _faker.Random.Int(1, 100),
                Titulo = _faker.Lorem.Paragraph()
            };
        }

        private LivroDto ObterDtoComAutorValido()
        {
            return new LivroDto()
            {
                AnoDePublicacao = _faker.Random.Int(1990, 2020),
                AutorId = Constantes.Um,
                Edicao = _faker.Random.Int(1, 10),
                Id = _faker.Random.Int(1, 100),
                Titulo = _faker.Lorem.Paragraph()
            };
        }
    }
}
