using Moq;
using System.Threading.Tasks;
using TheoLivraria.Dominio.IRepositories;
using TheoLivraria.Historia.Livros;
using TheoLivraria.Teste.Mocks;
using Xunit;

namespace TheoLivraria.Teste.Historias
{
    public class CriarLivroTeste
    {
        [Fact]
        public async Task DeveCriarUmLivro()
        {
            //Arrange
            var persistenciaDoLivroMock = new Mock<ILivroRepository>();
            persistenciaDoLivroMock.Setup(x => x.Criar(ModelsMock.LivroMock()));
            var criarLivro = new CriarLivro(persistenciaDoLivroMock.Object);

            //Action
            await criarLivro.Executar(ModelsMock.LivroMock());

            //Assert
            Assert.Empty(criarLivro.Erros);
        }
    }
}
