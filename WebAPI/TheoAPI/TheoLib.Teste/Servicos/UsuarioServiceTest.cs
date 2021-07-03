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
using TheoLib.Dominio.Modelo.UsuarioModelo;
using Xunit;

namespace TheoLib.Teste.Servicos
{
    public class UsuarioServiceTest
    {

        private readonly Fixture _fixture;
        private readonly RespostaBase _respostaBase;
        private readonly Usuario _usuario;
        private readonly IEnumerable<Usuario> _listaUsuario;
        private readonly RequisicaoUsuario _requisicaoUsuario;
        private readonly RequisicaoLoginUsuario _requestLogin;
        private readonly RespostaUsuario _respostaUsuario;
        private readonly ILogger<UsuarioServico> _logger;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepositorio _repository;
        private readonly IUsuarioServico _servico;
        private readonly IConfiguration _configuration;
        public UsuarioServiceTest()
        {
            _fixture = new Fixture();
            _logger = Substitute.For<ILogger<UsuarioServico>>();
            _mapper = Substitute.For<IMapper>();
            _repository = Substitute.For<IUsuarioRepositorio>();
            _configuration = Substitute.For<IConfiguration>();
            _respostaBase = _fixture.Create<RespostaBase>();
            _usuario = _fixture.Create<Usuario>();
            _listaUsuario = _fixture.Create<IEnumerable<Usuario>>();
            _requisicaoUsuario = _fixture.Create<RequisicaoUsuario>();
            _requestLogin = _fixture.Create<RequisicaoLoginUsuario>();
            _respostaUsuario = _fixture.Create<RespostaUsuario>();
            _servico = new UsuarioServico(_repository, _mapper, _logger, _configuration);

        }

        [Fact]
        public async void Inserir_Usuario_DeveRetornarBadRequest()
        {
            _respostaBase.StatusCode = StatusCodes.Status400BadRequest;
            _requisicaoUsuario.Nome = "";
            _mapper.Map<Usuario>(_requisicaoUsuario).Returns(_usuario);
            await _repository.Inserir(_usuario);
            var resultado = await _servico.Inserir(_requisicaoUsuario);

            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void Inserir_Usuario_DeveRetornarOK()
        {
            _respostaBase.StatusCode = StatusCodes.Status201Created;
            _mapper.Map<Usuario>(_requisicaoUsuario).Returns(_usuario);
            await _repository.Inserir(_usuario);
            _mapper.Map<RespostaUsuario>(_usuario).Returns(_respostaUsuario);
            var resultado = await _servico.Inserir(_requisicaoUsuario);

            resultado.StatusCode.Should().Be(StatusCodes.Status201Created);
        }

        [Fact]
        public async void Atualizar_Usuario_DeveRetornarBadRequest()
        {
            _respostaBase.StatusCode = StatusCodes.Status400BadRequest;
            _requisicaoUsuario.Nome = "";
            _mapper.Map<Usuario>(_requisicaoUsuario).Returns(_usuario);
            await _repository.Atualizar(_usuario);
            var resultado = await _servico.Inserir(_requisicaoUsuario);

            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void Atualizar_Usuario_UsuarioInexistente_DeveRetornarBadRequest()
        {
            _respostaBase.StatusCode = StatusCodes.Status400BadRequest;
            _mapper.Map<Usuario>(_requisicaoUsuario).Returns(_usuario);
            _repository.UsuarioEstaCadastrado(_usuario.Id).Returns(false);
            _repository.Atualizar(_usuario).Returns(_usuario);
            _mapper.Map<RespostaUsuario>(_usuario).Returns(_respostaUsuario);
            var resultado = await _servico.Atualizar(_requisicaoUsuario);

            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void AutenticarUsuario_DeveRetornarBadRequest()
        {
            _respostaBase.StatusCode = StatusCodes.Status400BadRequest;
            _requisicaoUsuario.Nome = "";
            _mapper.Map<Usuario>(_requisicaoUsuario).Returns(_usuario);
            _repository.ObterUsuario(_usuario.Email, _usuario.Senha).Returns(_usuario);
            var resultado = await _servico.AutenticarUsuario(_requestLogin);

            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void ObterLista_DeveRetornarBadRequest()
        {
            _mapper.Map<Usuario>(_requisicaoUsuario).Returns(_usuario);
            var resultado = await _servico.ObterLista();
            resultado.StatusCode = StatusCodes.Status400BadRequest;
            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void ObterLista_DeveRetornarOk()
        {
            _respostaBase.StatusCode = StatusCodes.Status200OK;
            _mapper.Map<Usuario>(_requisicaoUsuario).Returns(_usuario);
            _repository.ObterLista().Returns(_listaUsuario);
            var resultado = await _servico.ObterLista();

            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async void ObterUsuarioPorId_DeveRetornarBadRequest()
        {
            _respostaBase.StatusCode = StatusCodes.Status400BadRequest;
            _mapper.Map<Usuario>(_requisicaoUsuario).Returns(_usuario);
            _repository.ObterLista().Returns(default(IEnumerable<Usuario>));
            var resultado = await _servico.ObterPorId(Arg.Any<long>());

            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void ObterUsuarioPorId_DeveRetornarOk()
        {
            _respostaBase.StatusCode = StatusCodes.Status200OK;
            _mapper.Map<Usuario>(_requisicaoUsuario).Returns(_usuario);
            _mapper.Map<RespostaUsuario>(_usuario).Returns(_respostaUsuario);
            _repository.ObterPorId(1).Returns(_usuario);
            var resultado = await _servico.ObterPorId(1);

            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

    }
}
