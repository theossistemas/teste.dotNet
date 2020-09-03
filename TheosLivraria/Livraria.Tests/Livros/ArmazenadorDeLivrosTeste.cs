using Bogus;
using Livraria.Common.Handler;
using Livraria.Common.Implementation;
using Livraria.Common.Utils;
using Livraria.Domain.Dto;
using Livraria.Domain.Entidades;
using Livraria.Domain.Interfaces.Alteradores;
using Livraria.Domain.Interfaces.Repository;
using Livraria.Domain.Interfaces.Validadores;
using Livraria.Domain.Serviços.Armazenadores;
using Livraria.Tests.Comum;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Livraria.Tests.Livros
{
    public class ArmazenadorDeLivrosTeste
    {
        private NotifiyHandler _notifiyHandler;
        private Notify _notify;
        private readonly Faker _faker;
        private readonly Mock<ILivroRepositorio> _livroRepositorioMock;
        private readonly Mock<IValidadorDelivro> _validadorDeLivroMock;
        private readonly Mock<IAlteradorDeLivro> _alteradorDeLivroMock;
        private ArmazenadorDeLivro _armazenadorDeLivros;

        public ArmazenadorDeLivrosTeste()
        {
            _notifiyHandler = new NotifiyHandler();
            _notify = new Notify(_notifiyHandler);
            _faker = FakerBuilder.Novo().Build();
            _livroRepositorioMock = new Mock<ILivroRepositorio>();
            _validadorDeLivroMock = new Mock<IValidadorDelivro>();
            _alteradorDeLivroMock = new Mock<IAlteradorDeLivro>();
            _armazenadorDeLivros = new ArmazenadorDeLivro(
                _notify,
                _livroRepositorioMock.Object,
                _validadorDeLivroMock.Object,
                _alteradorDeLivroMock.Object);
        }

        [Fact]
        public async Task DeveArmazenarLivro()
        {
            //arrange
            var dto = ObterDtoComAutorValido();
            Livro livro = null;
            _livroRepositorioMock.Setup(x => x.ObterPorTitulo(It.IsAny<string>())).ReturnsAsync(livro);

            //act
            await _armazenadorDeLivros.Armazenar(dto);

            //assert
            _livroRepositorioMock.Verify(x => x.AdicionarAsync(It.IsAny<Livro>()), Times.Once);
        }

        [Fact]
        public async Task NaoDeveArmazenarLivro()
        {
            //arrange
            var dto = ObterDtoComAutorValido();
            Livro livro = LivroBuilder.Novo().Build();
            _notify.Invoke().NewNotification(Resources.LivroEntidade, Resources.LivroJaExiste);
            _livroRepositorioMock.Setup(x => x.ObterPorTitulo(It.IsAny<string>())).ReturnsAsync(livro);
            _armazenadorDeLivros = new ArmazenadorDeLivro(
                _notify,
                _livroRepositorioMock.Object,
                _validadorDeLivroMock.Object,
                _alteradorDeLivroMock.Object);

            //act
            await _armazenadorDeLivros.Armazenar(dto);

            //assert
            _livroRepositorioMock.Verify(x => x.AdicionarAsync(It.IsAny<Livro>()), Times.Never);
        }

        private LivroDto ObterDtoComAutorValido()
        {
            return new LivroDto()
            {
                AnoDePublicacao = _faker.Random.Int(1990, 2020),
                AutorId = Constantes.Um,
                Edicao = _faker.Random.Int(1, 10),
                Id = Constantes.Zero,
                Titulo = _faker.Lorem.Paragraph()
            };
        }
    }
}
