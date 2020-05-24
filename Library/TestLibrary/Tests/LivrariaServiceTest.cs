using Business.Services;
using Moq;
using NUnit.Framework;
using Persistence.Entity;
using Persistence.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using TestLibrary.Mock;

namespace TestLibrary.Tests
{
    public class LivrariaServiceTest
    {

        private LivrosServices _livroService;
        private Mock<ILivroRepository> _livroRepositoryMock;
        private List<Livro> _livroMock;
        private const int primeiroItemLista = 0;

        [SetUp]
        public void Setup()
        {
            _livroRepositoryMock = new Mock<ILivroRepository>();
            _livroService = new LivrosServices(_livroRepositoryMock.Object);
            _livroMock = LivroTestMock.ObterLivroMock();
        }

        [Test]
        public void ObterTodosRegistros()
        {
            //arrange
            _livroRepositoryMock.Setup(x => x.Get()).Returns(LivroTestMock.ObterLivroMock);

            //act
            int quantidadeResultado = _livroService.GetAll().Count();

            //assert
            Assert.AreEqual(_livroMock.Count, quantidadeResultado);
        }

        [Test]
        public void ObterRegistroPorId()
        {
            //arrange
            _livroRepositoryMock.Setup(x => x.GetById(1)).Returns(_livroMock[primeiroItemLista]);

            //act
            var actionResult = _livroService.GetById(1);

            //assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(actionResult);
                Assert.AreEqual(actionResult.Id, _livroMock[primeiroItemLista].Id);
            });
        }

        [Test]
        public void AlterarRegistro()
        {
            //arrange
            var registroAntigo = _livroMock[primeiroItemLista];
            var registroNovo = new Livro()
            {
                Id = 1,
                Descricao = "livro Alterado",
                Ano = "9/11/2011",
                Autor = "Desconhecido"
            };
            _livroRepositoryMock.Setup(x => x.GetById(1)).Returns(_livroMock[primeiroItemLista]);
            _livroRepositoryMock.Setup(x => x.Edit(registroAntigo, registroNovo)).Returns(registroNovo);

            //act
            var actionResult = _livroService.EditLivro(1, registroNovo);

            //assert
            Assert.AreNotEqual(registroAntigo, actionResult);
        }
    }
}