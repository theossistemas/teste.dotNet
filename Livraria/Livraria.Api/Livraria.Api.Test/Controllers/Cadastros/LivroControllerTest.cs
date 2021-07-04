using AutoMapper;
using Livraria.Api.AutoMapper;
using Livraria.Api.Controllers.Cadastros;
using Livraria.Api.Test.Helper;
using Livraria.Api.Test.RepositorioMock.Administracao;
using Livraria.Api.Test.RepositorioMock.Cadastros;
using Livraria.Api.Test.ServicesMock.Cadastros;
using Livraria.Domain.Dto.Cadastros;
using Livraria.Domain.Entities.Cadastros;
using Livraria.Infra.Data.Interfaces.Repositories.Administracao;
using Livraria.Infra.Data.Interfaces.Repositories.Cadastros;
using Livraria.Services.Interfaces.Cadastros;
using Livraria.Util.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Livraria.Api.Test.Controllers.Cadastros
{
    public class LivroControllerTest
    {
        private const string IDENTITY_USER = "Mocked User";
        private IMapper _mapper;
        private ILivroRepositorio _livroRepositorio;
        private ILogRepositorio _logRepositorio;
        private ILivroService _livroService;
        private ILivroService _livroServiceComRepositorios;
        private ILivroService _livroServiceException;
        private ILivroService _livroServiceAggregateException;
        private ILogger<LivroController> _logger;

        [SetUp]
        public void Setup()
        {
            _mapper = CriarMapper();
            _logRepositorio = new LogRepositorioMock();
            _livroRepositorio = new LivroRepositorioMock();
            _livroService = new LivroServiceMock();
            _livroServiceComRepositorios = new LivroServiceMock(_livroRepositorio);
            _livroServiceException = new LivroServiceExceptionMock();
            _livroServiceAggregateException = new LivroServiceAggregateExceptionMock();
            _logger = LoggerHelper.GetLogger<LivroController>(_logRepositorio).Object;
        }

        private void LimparRepositorios()
        {
            _logRepositorio.DeleteAll().RunSynchronously();
            _livroRepositorio.DeleteAll().RunSynchronously();
        }

        private LivroController InstanciarController(ILivroService livroService)
        {
            var controller = new LivroController(_mapper, livroService, _logger)
            {
                ControllerContext = GerarMockDoContexto()
            };
            return controller;
        }

        private static ControllerContext GerarMockDoContexto()
        {
            var fakeIdentity = new GenericIdentity("Mocked User");
            var context = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(fakeIdentity)
                }
            };

            return context;
        }

        private static Mapper CriarMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<LivroProfileMap>();
            });
            return new Mapper(mapperConfiguration);
        }

        [Test]
        public async Task Deve_lancar_exception_ao_tentar_consultar_por_id()
        {
            var livroController = InstanciarController(_livroServiceException);
            await livroController.Consultar(1);

            var log = _logRepositorio.GetAll().ToList();

            Assert.AreEqual(1, log.Count);
            Assert.AreEqual($"Erro ao tentar consultar o livro 1: Teste Exception ConsultarPorId  - Usuário: {IDENTITY_USER}", log.FirstOrDefault().Message);
            Assert.AreEqual("Error", log.FirstOrDefault().Level);

            LimparRepositorios();
        }

        [Test]
        public async Task Deve_lancar_aggregate_exception_ao_tentar_consultar_por_id()
        {
            var livroController = InstanciarController(_livroServiceAggregateException);

            await livroController.Consultar(1);

            var log = _logRepositorio.GetAll().ToList();

            Assert.AreEqual(1, log.Count);
            Assert.AreEqual($"Erro ao tentar consultar o livro 1: Teste aggregate exceptions ConsultarPorId (Exceção agregada 1) (Exceção agregada 2) Exceção agregada 1; Exceção agregada 2;  - Usuário: {IDENTITY_USER}", log.FirstOrDefault().Message);
            Assert.AreEqual("Error", log.FirstOrDefault().Level);

            LimparRepositorios();
        }

        [Test]
        public void Deve_retornar_o_livro_ao_consultar_por_id()
        {
            var livroController = InstanciarController(_livroService);

            var okResult = livroController.Consultar(1).Result.Result as OkObjectResult;
            var livro = okResult.Value as LivroDto;
            Assert.AreEqual("J. R. R. Tolkien.", livro.Autor);
            Assert.AreEqual("4", livro.Genero);
            Assert.AreEqual("O Senhor dos Aneis - A sociedade do anel", livro.Titulo);
        }

        [Test]
        public async Task Deve_lancar_exception_ao_tentar_consultar_todos()
        {
            var livroController = InstanciarController(_livroServiceException);
            await livroController.ConsultarTodosAscendente();

            var log = _logRepositorio.GetAll().ToList();

            Assert.AreEqual(1, log.Count);
            Assert.AreEqual($"Erro ao tentar consultar todos os livros: Teste Exception ConsultarTodosOrderByAsc  - Usuário: {IDENTITY_USER}", log.FirstOrDefault().Message);
            Assert.AreEqual("Error", log.FirstOrDefault().Level);

            LimparRepositorios();
        }

        [Test]
        public async Task Deve_lancar_aggregate_exception_ao_tentar_consultar_todos()
        {
            var livroController = InstanciarController(_livroServiceAggregateException);
            await livroController.ConsultarTodosAscendente();

            var log = _logRepositorio.GetAll().ToList();

            Assert.AreEqual(1, log.Count);
            Assert.AreEqual($"Erro ao tentar consultar todos os livros: Teste aggregate exceptions ConsultarTodosOrderByAsc (Exceção agregada 1) (Exceção agregada 2) Exceção agregada 1; Exceção agregada 2;  - Usuário: {IDENTITY_USER}", log.FirstOrDefault().Message);
            Assert.AreEqual("Error", log.FirstOrDefault().Level);

            LimparRepositorios();
        }

        [Test]
        public void Deve_retornar_o_lista_de_livros_ao_consultar_todos_ascendente()
        {
            var livroController = InstanciarController(_livroService);

            var okResult = livroController.ConsultarTodosAscendente().Result.Result as OkObjectResult;
            var livros = okResult.Value as List<LivroDto>;
            Assert.AreEqual(3, livros.Count);
            Assert.AreEqual("O Senhor dos Aneis - A sociedade do anel", livros.First().Titulo);
            Assert.AreEqual("O Senhor dos Aneis - O retorno do rei", livros.Last().Titulo);
        }

        [Test]
        public async Task Deve_lancar_exception_ao_tentar_cadastrar()
        {
            var livroController = InstanciarController(_livroServiceException);

            var titulo = "O Senhor dos Aneis - A sociedade do anel";
            var autor = "J. R. R. Tolkien";
            var genero = "4";

            var livroParaCadastrar = new LivroDto
            {
                Autor = autor,
                Genero = genero,
                Titulo = titulo
            };

            await livroController.Cadastrar(livroParaCadastrar);

            var logs = _logRepositorio.GetAll().ToList();
            Assert.AreEqual(2, logs.Count);

            var logInformation = logs.Where(x => x.Level == "Information").FirstOrDefault();
            Assert.AreEqual($"Solicitação de cadastro de um livro. Autor: {autor}; Gênero: {genero}; Título: {titulo} - Usuário: {IDENTITY_USER}", logInformation.Message);

            var logErro = logs.Where(x => x.Level == "Error").FirstOrDefault();
            Assert.AreEqual($"Erro ao tentar cadastrar um livro. Autor: {autor}; Gênero: {genero}; Título: {titulo}: Teste Exception InserirNovo  - Usuário: {IDENTITY_USER}", logErro.Message);

            LimparRepositorios();
        }

        [Test]
        public async Task Deve_lancar_aggregate_exception_ao_tentar_cadastrar()
        {
            var livroController = InstanciarController(_livroServiceAggregateException);

            var titulo = "O Senhor dos Aneis - A sociedade do anel";
            var autor = "J. R. R. Tolkien";
            var genero = "4";

            var livroParaCadastrar = new LivroDto
            {
                Autor = autor,
                Genero = genero,
                Titulo = titulo
            };

            await livroController.Cadastrar(livroParaCadastrar);

            var logs = _logRepositorio.GetAll().ToList();
            Assert.AreEqual(2, logs.Count);

            var logInformation = logs.Where(x => x.Level == "Information").FirstOrDefault();
            Assert.AreEqual($"Solicitação de cadastro de um livro. Autor: {autor}; Gênero: {genero}; Título: {titulo} - Usuário: {IDENTITY_USER}", logInformation.Message);

            var logErro = logs.Where(x => x.Level == "Error").FirstOrDefault();
            Assert.AreEqual($"Erro ao tentar cadastrar um livro. Autor: {autor}; Gênero: {genero}; Título: {titulo}: Teste aggregate exceptions InserirNovo (Exceção agregada 1) (Exceção agregada 2) Exceção agregada 1; Exceção agregada 2;  - Usuário: {IDENTITY_USER}", logErro.Message);

            LimparRepositorios();
        }

        [Test]
        public async Task Deve_cadastrar_um_livro()
        {
            var livroController = InstanciarController(_livroServiceComRepositorios);

            var titulo = "O Senhor dos Aneis - A sociedade do anel";
            var autor = "J. R. R. Tolkien";
            var genero = "4";

            var livroParaCadastrar = new LivroDto
            {
                Autor = autor,
                Genero = genero,
                Titulo = titulo
            };

            await livroController.Cadastrar(livroParaCadastrar);

            var logs = _logRepositorio.GetAll().ToList();
            Assert.AreEqual(1, logs.Count);

            var logInformation = logs.Where(x => x.Level == "Information").FirstOrDefault();
            Assert.AreEqual($"Solicitação de cadastro de um livro. Autor: {autor}; Gênero: {genero}; Título: {titulo} - Usuário: {IDENTITY_USER}", logInformation.Message);

            var livrosCriados = _livroRepositorio.GetAll().ToList();
            Assert.AreEqual(1, livrosCriados.Count());

            var livroCriado = livrosCriados.First();
            Assert.AreEqual(autor, livroCriado.Autor);
            Assert.AreEqual(genero.TryToInt(), livroCriado.GeneroId);
            Assert.AreEqual(titulo, livroCriado.Titulo);

            LimparRepositorios();
        }

        [Test]
        public async Task Deve_lancar_exception_ao_tentar_alterar_um_livro()
        {
            var livroController = InstanciarController(_livroServiceException);

            var titulo = "O Senhor dos Aneis - A sociedade do anel";
            var autor = "J. R. R. Tolkien";
            var genero = "4";
            var codigo = "1";

            var livroParaAlterar = new LivroDto
            {
                Autor = autor,
                Genero = genero,
                Titulo = titulo,
                Codigo = codigo
            };

            await livroController.Alterar(1, livroParaAlterar);

            var logs = _logRepositorio.GetAll().ToList();
            Assert.AreEqual(2, logs.Count);

            var logWarning = logs.Where(x => x.Level == "Warning").FirstOrDefault();
            Assert.AreEqual($"Solicitação de alteração do livro {codigo} - Usuário: {IDENTITY_USER}", logWarning.Message);

            var logErro = logs.Where(x => x.Level == "Error").FirstOrDefault();
            Assert.AreEqual($"Erro ao tentar alterar o cadastro de um livro. Código: {codigo}; Autor: {autor}; Gênero: {genero}; Título: {titulo}: Teste Exception AlterarLivro  - Usuário: {IDENTITY_USER}", logErro.Message);

            LimparRepositorios();
        }

        [Test]
        public async Task Deve_lancar_aggregate_exception_ao_tentar_alterar_um_livro()
        {
            var livroController = InstanciarController(_livroServiceException);

            var titulo = "O Senhor dos Aneis - A sociedade do anel";
            var autor = "J. R. R. Tolkien";
            var genero = "4";
            var codigo = "1";

            var livroParaAlterar = new LivroDto
            {
                Autor = autor,
                Genero = genero,
                Titulo = titulo,
                Codigo = codigo
            };

            await livroController.Alterar(1, livroParaAlterar);

            var logs = _logRepositorio.GetAll().ToList();
            Assert.AreEqual(2, logs.Count);

            var logWarning = logs.Where(x => x.Level == "Warning").FirstOrDefault();
            Assert.AreEqual($"Solicitação de alteração do livro {codigo} - Usuário: {IDENTITY_USER}", logWarning.Message);

            var logErro = logs.Where(x => x.Level == "Error").FirstOrDefault();
            Assert.AreEqual($"Erro ao tentar alterar o cadastro de um livro. Código: {codigo}; Autor: {autor}; Gênero: {genero}; Título: {titulo}: Teste Exception AlterarLivro  - Usuário: {IDENTITY_USER}", logErro.Message);

            LimparRepositorios();
        }

        [Test]
        public async Task Deve_alterar_um_livro()
        {
            var titulo = "O Senhor dos Aneis - A sociedade do anel";
            var autor = "J. R. R. Tolkien";
            var genero = "4";
            var codigo = "1";

            var livroParaCadastrar = CriarLivro(titulo, autor, genero, codigo);
            InserirLivroNoRepositorio(livroParaCadastrar);

            var novoTitulo = "O Senhor dos Aneis - As duas torres";
            var livroParaAlterar = CriarLivroDto(novoTitulo, autor, genero, codigo);

            var livroController = InstanciarController(_livroServiceComRepositorios);
            await livroController.Alterar(codigo.TryToInt(), livroParaAlterar);

            var logs = _logRepositorio.GetAll().ToList();
            Assert.AreEqual(1, logs.Count);

            var logWarning = logs.Where(x => x.Level == "Warning").FirstOrDefault();
            Assert.AreEqual($"Solicitação de alteração do livro 1 - Usuário: {IDENTITY_USER}", logWarning.Message);

            var livrosCriados = _livroRepositorio.GetAll().ToList();
            Assert.AreEqual(1, livrosCriados.Count());

            var livroAlterado = livrosCriados.First();
            Assert.AreEqual(autor, livroAlterado.Autor);
            Assert.AreEqual(genero.TryToInt(), livroAlterado.GeneroId);
            Assert.AreEqual(novoTitulo, livroAlterado.Titulo);

            LimparRepositorios();
        }

        [Test]
        public async Task Deve_lancar_exception_ao_tentar_excluir_um_livro()
        {
            var livroController = InstanciarController(_livroServiceException);

            await livroController.Excluir(1);

            var logs = _logRepositorio.GetAll().ToList();
            Assert.AreEqual(2, logs.Count);

            var logWarning = logs.Where(x => x.Level == "Warning").FirstOrDefault();
            Assert.AreEqual($"Solicitação de exclusão do livro 1 - Usuário: {IDENTITY_USER}", logWarning.Message);

            var logErro = logs.Where(x => x.Level == "Error").FirstOrDefault();
            Assert.AreEqual($"Erro ao tentar excluir o livro: 1. Teste Exception ExcluirLivro  - Usuário: {IDENTITY_USER}", logErro.Message);

            LimparRepositorios();
        }

        [Test]
        public async Task Deve_lancar_aggregate_exception_ao_tentar_excluir_um_livro()
        {
            var livroController = InstanciarController(_livroServiceAggregateException);

            await livroController.Excluir(1);

            var logs = _logRepositorio.GetAll().ToList();
            Assert.AreEqual(2, logs.Count);

            var logWarning = logs.Where(x => x.Level == "Warning").FirstOrDefault();
            Assert.AreEqual($"Solicitação de exclusão do livro 1 - Usuário: {IDENTITY_USER}", logWarning.Message);

            var logErro = logs.Where(x => x.Level == "Error").FirstOrDefault();
            Assert.AreEqual($"Erro ao tentar excluir o livro: 1. Teste aggregate exceptions ExcluirLivro (Exceção agregada 1) (Exceção agregada 2) Exceção agregada 1; Exceção agregada 2;  - Usuário: {IDENTITY_USER}", logErro.Message);

            LimparRepositorios();
        }

        [Test]
        public async Task Deve_excluir_um_livro()
        {
            var titulo = "O Senhor dos Aneis - A sociedade do anel";
            var autor = "J. R. R. Tolkien";
            var genero = "4";
            var codigo = "1";

            var livroParaCadastrar = CriarLivro(titulo, autor, genero, codigo);
            InserirLivroNoRepositorio(livroParaCadastrar);

            var livroController = InstanciarController(_livroServiceComRepositorios);
            await livroController.Excluir(codigo.TryToInt());

            var logs = _logRepositorio.GetAll().ToList();
            Assert.AreEqual(1, logs.Count);

            var logWarning = logs.Where(x => x.Level == "Warning").FirstOrDefault();
            Assert.AreEqual($"Solicitação de exclusão do livro 1 - Usuário: {IDENTITY_USER}", logWarning.Message);

            var livrosRepositorio = _livroRepositorio.GetAll().ToList();
            Assert.AreEqual(0, livrosRepositorio.Count());
            
            LimparRepositorios();
        }

        private void InserirLivroNoRepositorio(Livro livroParaCacdastrar)
        {
            _livroRepositorio.Create(livroParaCacdastrar).RunSynchronously();
        }

        private static Livro CriarLivro(string titulo, string autor, string genero, string codigo)
        {
            return new Livro
            {
                Autor = autor,
                GeneroId = genero.TryToInt(),
                Titulo = titulo,
                Id = codigo.TryToInt()
            };
        }

        private static LivroDto CriarLivroDto(string titulo, string autor, string genero, string codigo)
        {
            return new LivroDto
            {
                Autor = autor,
                Genero = genero,
                Titulo = titulo,
                Codigo = codigo
            };
        }
    }
}
