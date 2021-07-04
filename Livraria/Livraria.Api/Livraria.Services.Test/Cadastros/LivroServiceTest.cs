using Livraria.Infra.Data.Interfaces.Repositories.Cadastros;
using Livraria.Services.Test.Environments.Cadastros;
using Livraria.Services.Test.RepositorioMock.Cadastros;
using NUnit.Framework;

namespace Livraria.Services.Test.Cadastros
{
    public class LivroServiceTest
    {
        private ILivroRepositorio _livroRepositorio;

        [SetUp]
        public void Setup()
        {
            _livroRepositorio = new LivroRepositorioMock();
        }

        [Test]
        public void Deve_consultar_por_id()
        {
            LivroServiceEnvironment.Configurar(_livroRepositorio);
            var livro = _livroRepositorio.GetById(1).Result;
            Assert.AreEqual("J. R. R. Tolkien.", livro.Autor);
            Assert.AreEqual("O Senhor dos Aneis - A sociedade do anel", livro.Titulo);
            Assert.AreEqual(4, livro.GeneroId);
        }
    }
}
