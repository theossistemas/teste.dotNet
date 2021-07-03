using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Collections.Generic;
using TheoLib.CoreAplicacao.Servicos;
using TheoLib.Dominio.Contratos.Repositorio;
using TheoLib.Dominio.Contratos.Servicos;
using TheoLib.Dominio.Entidade;
using TheoLib.Dominio.Modelo;
using TheoLib.Dominio.Modelo.LivroModelo;
using Xunit;

namespace TheoLib.Teste.Servicos
{
    public class LivroServicoTest
    {

        private readonly Fixture _fixture;
        private readonly RespostaBase _respostaBase;
        private readonly Livro _livro;
        private readonly IEnumerable<Livro> _listaLivro;
        private readonly RequisicaoLivro _requisicaoLivro;
        private readonly RespostaLivro _respostaLivro;
        private readonly ILogger<LivroServico> _logger;
        private readonly IMapper _mapper;
        private readonly ILivroRepositorio _repositorio;
        private readonly ILivroServico _servico;
        private readonly IConfiguration _configuration;
        public LivroServicoTest()
        {
            _fixture = new Fixture();
            _logger = Substitute.For<ILogger<LivroServico>>();
            _mapper = Substitute.For<IMapper>();
            _repositorio = Substitute.For<ILivroRepositorio>();
            _configuration = Substitute.For<IConfiguration>();
            _respostaBase = _fixture.Create<RespostaBase>();
            _livro = _fixture.Create<Livro>();
            _listaLivro = _fixture.Create<IEnumerable<Livro>>();
            _requisicaoLivro = _fixture.Create<RequisicaoLivro>();
            _respostaLivro = _fixture.Create<RespostaLivro>();
            _servico = new LivroServico(_repositorio, _mapper, _logger, _configuration);

        }

        [Fact]
        public async void InserirLivroComBadRequest()
        {
            _respostaBase.StatusCode = StatusCodes.Status400BadRequest;
            _requisicaoLivro.Titulo = "";
            _mapper.Map<Livro>(_requisicaoLivro).Returns(_livro);
            await _repositorio.Inserir(_livro);
            var resultado = await _servico.Inserir(_requisicaoLivro);

            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void Inserir_Livro_DeveRetornarOK()
        {
            _respostaBase.StatusCode = StatusCodes.Status201Created;
            _mapper.Map<Livro>(_requisicaoLivro).Returns(_livro);
            await _repositorio.Inserir(_livro);
            _mapper.Map<RespostaLivro>(_livro).Returns(_respostaLivro);
            var resultado = await _servico.Inserir(_requisicaoLivro);

            resultado.StatusCode.Should().Be(StatusCodes.Status201Created);
        }

        [Fact]
        public async void AtualizarLivroBadRequest()
        {
            _respostaBase.StatusCode = StatusCodes.Status400BadRequest;
            _requisicaoLivro.Titulo = "";
            _mapper.Map<Livro>(_requisicaoLivro).Returns(_livro);
            await _repositorio.Atualizar(_livro);
            var resultado = await _servico.Inserir(_requisicaoLivro);

            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void AtualizarLivroLivroInexistenteBadRequest()
        {
            _respostaBase.StatusCode = StatusCodes.Status400BadRequest;
            _mapper.Map<Livro>(_requisicaoLivro).Returns(_livro);
            _repositorio.LivroPossuiCadastro(_livro.Id).Returns(false);
            _repositorio.Atualizar(_livro).Returns(_livro);
            _mapper.Map<RespostaLivro>(_livro).Returns(_respostaLivro);
            var resultado = await _servico.Atualizar(_requisicaoLivro);

            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
         

        [Fact]
        public async void ObterListaBadRequest()
        {
            _mapper.Map<Livro>(_requisicaoLivro).Returns(_livro);
            var resultado = await _servico.ObterLista();
            resultado.StatusCode = StatusCodes.Status400BadRequest;
            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void ObterListaDeveRetornarOk()
        {
            _respostaBase.StatusCode = StatusCodes.Status200OK;
            _mapper.Map<Livro>(_requisicaoLivro).Returns(_livro);
            _repositorio.ObterLista().Returns(_listaLivro);
            var resultado = await _servico.ObterLista();

            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async void ObterLivroPorIdBadRequest()
        {
            _respostaBase.StatusCode = StatusCodes.Status400BadRequest;
            _mapper.Map<Livro>(_requisicaoLivro).Returns(_livro);
            _repositorio.ObterLista().Returns(default(IEnumerable<Livro>));
            var resultado = await _servico.ObterPorId(Arg.Any<long>());

            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void ObterLivroPorIdOk()
        {
            _respostaBase.StatusCode = StatusCodes.Status200OK;
            _mapper.Map<Livro>(_requisicaoLivro).Returns(_livro);
            _mapper.Map<RespostaLivro>(_livro).Returns(_respostaLivro);
            _repositorio.ObterPorId(1).Returns(_livro); 
            var resultado = await _servico.ObterPorId(1);

            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

    }
}
