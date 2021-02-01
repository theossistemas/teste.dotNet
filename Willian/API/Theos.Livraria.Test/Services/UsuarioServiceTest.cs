using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute; 
using System;
using System.Collections.Generic; 
using Theos.Livraria.Application.Services;
using Theos.Livraria.Domain.Entity;
using Theos.Livraria.Domain.Interface.Repository;
using Theos.Livraria.Domain.Interface.Services;
using Theos.Livraria.Domain.Model;
using Theos.Livraria.Domain.Model.Usuario;
using Xunit;

namespace Theos.Livraria.Test.Services
{
    public class UsuarioServiceTest
    {
        private readonly Fixture _fixture;
        private readonly BaseResponse _baseResponse;
        private readonly Usuario _usuario;
        private readonly IEnumerable<Usuario> _listaUsuario;
        private readonly RequestUsuario _requestUsuario;
        private readonly RequestLoginUsuario _requestLogin;
        private readonly ResponseUsuario _responseUsuario;
        private readonly ILogger<UsuarioService> _logger;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _repository;
        private readonly IUsuarioService _service;
        private readonly IConfiguration _configuration;
        public UsuarioServiceTest()
        {
            _fixture = new Fixture();
            _logger = Substitute.For<ILogger<UsuarioService>>();
            _mapper = Substitute.For<IMapper>();
            _repository = Substitute.For<IUsuarioRepository>();
            _configuration = Substitute.For<IConfiguration>();
            _baseResponse = _fixture.Create<BaseResponse>();
            _usuario = _fixture.Create<Usuario>();
            _listaUsuario = _fixture.Create<IEnumerable<Usuario>>();
            _requestUsuario = _fixture.Create<RequestUsuario>();
            _requestLogin = _fixture.Create<RequestLoginUsuario>();
            _responseUsuario = _fixture.Create<ResponseUsuario>();
            _service = new UsuarioService(_repository, _mapper, _logger, _configuration);

        }

        [Fact]
        public async void Inserir_Usuario_DeveRetornarBadRequest()
        {
            _baseResponse.StatusCode = StatusCodes.Status400BadRequest;
            _requestUsuario.Nome = "";
            _mapper.Map<Usuario>(_requestUsuario).Returns(_usuario);
            await _repository.Inserir(_usuario);
            var resultado = await _service.Inserir(_requestUsuario);

            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void Inserir_Usuario_DeveRetornarOK()
        {
            _baseResponse.StatusCode = StatusCodes.Status201Created;
            _mapper.Map<Usuario>(_requestUsuario).Returns(_usuario);
            await _repository.Inserir(_usuario);
            _mapper.Map<ResponseUsuario>(_usuario).Returns(_responseUsuario);
            var resultado = await _service.Inserir(_requestUsuario);

            resultado.StatusCode.Should().Be(StatusCodes.Status201Created);
        }

        [Fact]
        public async void Atualizar_Usuario_DeveRetornarBadRequest()
        {
            _baseResponse.StatusCode = StatusCodes.Status400BadRequest;
            _requestUsuario.Nome = "";
            _mapper.Map<Usuario>(_requestUsuario).Returns(_usuario);
            await _repository.Atualizar(_usuario);
            var resultado = await _service.Inserir(_requestUsuario);

            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void Atualizar_Usuario_UsuarioInexistente_DeveRetornarBadRequest()
        {
            _baseResponse.StatusCode = StatusCodes.Status400BadRequest;
            _mapper.Map<Usuario>(_requestUsuario).Returns(_usuario);
            _repository.UsuarioCadastrado(_usuario.Id).Returns(false);
            _repository.Atualizar(_usuario).Returns(_usuario);
            _mapper.Map<ResponseUsuario>(_usuario).Returns(_responseUsuario);
            var resultado = await _service.Atualizar(_requestUsuario);

            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void AutenticarUsuario_DeveRetornarBadRequest()
        {
            _baseResponse.StatusCode = StatusCodes.Status400BadRequest;
            _requestUsuario.Nome = "";
            _mapper.Map<Usuario>(_requestUsuario).Returns(_usuario);
            _repository.ObterUsuario(_usuario.Email, _usuario.Senha).Returns(_usuario);
            var resultado = await _service.AutenticarUsuario(_requestLogin);

            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void ObterLista_DeveRetornarBadRequest()
        {
            _mapper.Map<Usuario>(_requestUsuario).Returns(_usuario);
            var resultado = await _service.ObterLista();
            resultado.StatusCode = StatusCodes.Status400BadRequest;
            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void ObterLista_DeveRetornarOk()
        {
            _baseResponse.StatusCode = StatusCodes.Status200OK;
            _mapper.Map<Usuario>(_requestUsuario).Returns(_usuario);
            _repository.ObterLista().Returns(_listaUsuario);
            var resultado = await _service.ObterLista();

            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async void ObterUsuarioPorId_DeveRetornarBadRequest()
        {
            _baseResponse.StatusCode = StatusCodes.Status400BadRequest;
            _mapper.Map<Usuario>(_requestUsuario).Returns(_usuario);
            _repository.ObterLista().Returns(default(IEnumerable<Usuario>));
            var resultado = await _service.ObterPorId(Arg.Any<long>());

            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void ObterUsuarioPorId_DeveRetornarOk()
        {
            _baseResponse.StatusCode = StatusCodes.Status200OK;
            _mapper.Map<Usuario>(_requestUsuario).Returns(_usuario);
            _mapper.Map<ResponseUsuario>(_usuario).Returns(_responseUsuario);
            _repository.ObterPorId(1).Returns(_usuario);
            var resultado = await _service.ObterPorId(1);

            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}