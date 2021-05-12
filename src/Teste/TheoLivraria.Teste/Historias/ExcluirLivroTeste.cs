using Moq;
using System.Threading.Tasks;
using TheoLivraria.Dominio.IRepositories;
using TheoLivraria.Historia.Livros;
using TheoLivraria.Teste.Mocks;
using Xunit;

namespace TheoLivraria.Teste.Historias
{
    public class ExcluirLivroTeste
    {
        [Fact]
        public async Task DeveExcluirUmLivro()
        {
            //Arrange
            var persistenciaDoLivroMock = new Mock<ILivroRepository>();
            persistenciaDoLivroMock.Setup(x => x.Criar(ModelsMock.LivroMock()));

            var excluirLivro = new ExcluirLivro(persistenciaDoLivroMock.Object);

            //Action
            await excluirLivro.Executar(ModelsMock.LivroMock());

            //Assert
            Assert.Empty(excluirLivro.Erros);
        }
    }
}
