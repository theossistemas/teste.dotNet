using LivrariaWeb.Domain.Interface;
using LivrariaWeb.Service;
using LivrariaWeb.Service.Interface;
using LivrariaWeb.TDD.Builder;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace LivrariaWeb.TDD
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DeveValidarSeLivroJahFoiCadastrado()
        {
            LivroTestBuilder livroBuilder = new LivroTestBuilder();

            Mock<ILivroRepository> mockLivroRepository = new Mock<ILivroRepository>();
            Mock<ILogger<LivroService>> mockLoggerRepository = new Mock<ILogger<LivroService>>();
            Mock<ILivroService> mockLivroService = new Mock<ILivroService>();

            mockLivroRepository.Setup(x => x.VerificaSeLivroJaFoiCadastrado(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            LivroService cursoService = new LivroService(mockLivroRepository.Object, mockLoggerRepository.Object);

            var cursoResult = cursoService.Cadastrar(livroBuilder.ComLivro(It.IsAny<string>(), It.IsAny<string>()).BuildDto()).Result;
            Assert.AreEqual(cursoResult.Message, "Livro já consta em sua base de dados.");
        }

        [Test]
        public void DeveValidarSeLivroFoiCadastrado()
        {
            LivroTestBuilder livroBuilder = new LivroTestBuilder();

            Mock<ILivroRepository> mockLivroRepository = new Mock<ILivroRepository>();
            Mock<ILogger<LivroService>> mockLoggerRepository = new Mock<ILogger<LivroService>>();
            Mock<ILivroService> mockLivroService = new Mock<ILivroService>();

            mockLivroRepository.Setup(x => x.VerificaSeLivroJaFoiCadastrado(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            LivroService cursoService = new LivroService(mockLivroRepository.Object, mockLoggerRepository.Object);

            var cursoResult = cursoService.Cadastrar(livroBuilder.ComLivro(It.IsAny<string>(), It.IsAny<string>()).BuildDto()).Result;
            Assert.AreEqual(cursoResult.Message, "Livro cadastrado com sucesso.");
        }
        [Test]
        public void DeveValidarSeLivroFoiRetornado()
        {
            LivroTestBuilder livroBuilder = new LivroTestBuilder();

            Mock<ILivroRepository> mockLivroRepository = new Mock<ILivroRepository>();
            Mock<ILogger<LivroService>> mockLoggerRepository = new Mock<ILogger<LivroService>>();
            Mock<ILivroService> mockLivroService = new Mock<ILivroService>();

            mockLivroRepository.Setup(x => x.VerificaSeLivroExiste(It.IsAny<long>())).Returns(true);
            mockLivroRepository.Setup(x => x.GetEntityById(It.IsAny<long>())).Returns(Task.FromResult(livroBuilder.ComLivro(It.IsAny<string>(),
                                                                                                                            It.IsAny<string>(),
                                                                                                                            DateTime.Now,
                                                                                                                            It.IsAny<int>(),
                                                                                                                            It.IsAny<long>()).BuildLivro()));

            LivroService cursoService = new LivroService(mockLivroRepository.Object, mockLoggerRepository.Object);

            var cursoResult = cursoService.GetLivroById(It.IsAny<long>()).Result;
            Assert.AreEqual(cursoResult.Message, "Livro Existe.");
        }

        [Test]
        public void DeveValidarSeLivroNaoFoiRetornado()
        {
            LivroTestBuilder livroBuilder = new LivroTestBuilder();

            Mock<ILivroRepository> mockLivroRepository = new Mock<ILivroRepository>();
            Mock<ILogger<LivroService>> mockLoggerRepository = new Mock<ILogger<LivroService>>();
            Mock<ILivroService> mockLivroService = new Mock<ILivroService>();

            mockLivroRepository.Setup(x => x.VerificaSeLivroExiste(It.IsAny<long>())).Returns(false);
            mockLivroRepository.Setup(x => x.GetEntityById(It.IsAny<long>())).Returns(Task.FromResult(livroBuilder.ComLivro(It.IsAny<string>(),
                                                                                                                            It.IsAny<string>(),
                                                                                                                            DateTime.Now,
                                                                                                                            It.IsAny<int>(),
                                                                                                                            It.IsAny<long>()).BuildLivro()));

            LivroService cursoService = new LivroService(mockLivroRepository.Object, mockLoggerRepository.Object);

            var cursoResult = cursoService.GetLivroById(It.IsAny<long>()).Result;
            Assert.AreEqual(cursoResult.Message, "Livro não encontrado.");
        }

        [Test]
        public void DeveValidarSeLivroFoiAlterado()
        {
            LivroTestBuilder livroBuilder = new LivroTestBuilder();

            Mock<ILivroRepository> mockLivroRepository = new Mock<ILivroRepository>();
            Mock<ILogger<LivroService>> mockLoggerRepository = new Mock<ILogger<LivroService>>();
            Mock<ILivroService> mockLivroService = new Mock<ILivroService>();

            mockLivroRepository.Setup(x => x.GetEntityById(It.IsAny<long>())).Returns(Task.FromResult(livroBuilder.ComLivro(It.IsAny<string>(),
                                                                                                                            It.IsAny<string>(),
                                                                                                                            DateTime.Now,
                                                                                                                            It.IsAny<int>(),
                                                                                                                            It.IsAny<long>()).BuildLivro()));
            mockLivroRepository.Setup(x => x.VerificaSeLivroJaFoiCadastrado(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            LivroService cursoService = new LivroService(mockLivroRepository.Object, mockLoggerRepository.Object);

            var cursoResult = cursoService.UpdateLivro(livroBuilder.ComLivroDto(It.IsAny<string>(),
                                                                            It.IsAny<string>(),
                                                                            DateTime.Now,
                                                                            It.IsAny<int>(),
                                                                            It.IsAny<long>()).BuildDto()).Result;
            Assert.AreEqual(cursoResult.Message, "Livro alterado com sucesso.");
        }

        [Test]
        public void DeveValidarSeLivroNaoFoiAlterado()
        {
            LivroTestBuilder livroBuilder = new LivroTestBuilder();

            Mock<ILivroRepository> mockLivroRepository = new Mock<ILivroRepository>();
            Mock<ILogger<LivroService>> mockLoggerRepository = new Mock<ILogger<LivroService>>();
            Mock<ILivroService> mockLivroService = new Mock<ILivroService>();

            mockLivroRepository.Setup(x => x.GetEntityById(It.IsAny<long>())).Returns(Task.FromResult(livroBuilder.BuildLivroEmpty()));
            mockLivroRepository.Setup(x => x.VerificaSeLivroJaFoiCadastrado(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            LivroService cursoService = new LivroService(mockLivroRepository.Object, mockLoggerRepository.Object);

            var cursoResult = cursoService.UpdateLivro(livroBuilder.ComLivroDto(It.IsAny<string>(),
                                                                            It.IsAny<string>(),
                                                                            DateTime.Now,
                                                                            It.IsAny<int>(),
                                                                            It.IsAny<long>()).BuildDto()).Result;
            Assert.AreEqual(cursoResult.Message, "Livro não consta na base de dados.");
        }
    }
}