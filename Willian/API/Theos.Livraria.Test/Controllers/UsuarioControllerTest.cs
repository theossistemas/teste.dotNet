using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Theos.Livraria.Api.Controllers;
using Theos.Livraria.Domain.Interface.Services;
using Theos.Livraria.Domain.Model;
using Theos.Livraria.Domain.Model.Usuario;
using Xunit;

namespace Theos.Livraria.Test.Controllers
{
    public class UsuarioControllerTest
    {
        private readonly Fixture _fixture;
        private readonly RequestUsuario  _request;
        private readonly RequestLoginUsuario _requestLogin;
        private readonly BaseResponse _response;
        private readonly IUsuarioService _service;
        private readonly UsuarioController _controller;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioControllerTest()  
        {

            _fixture = new Fixture();
           
            _request = _fixture.Create<RequestUsuario>();
            _requestLogin = _fixture.Create<RequestLoginUsuario>();
            _response = _fixture.Create<BaseResponse>();
            _service = Substitute.For<IUsuarioService>();
            _logger = Substitute.For<ILogger<UsuarioController>>();

            _controller = new UsuarioController(_service, _logger);
        }

        [Fact]
        public async void Inserir_Usuario_QuandoTudoEstiverCorreto_DeveRetornarSucesso()
        {
            _response.StatusCode = StatusCodes.Status201Created;
            _service.Inserir(_request).Returns(_response);

            var result = (ObjectResult)await _controller.Inserir(_request);

            result.StatusCode.Should().Be(201);
        }

        [Fact]
        public async void Inserir_Usuario_QuandoFalhar_DeveRetornarBadRequest()
        {

            _response.StatusCode = StatusCodes.Status400BadRequest;
            _service.Inserir(_request).Returns(_response);

            var result = (ObjectResult)await _controller.Inserir(_request);

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async void Alterar_Usuario_QuandoTudoEstiverCorreto_DeveRetornarSucesso()
        {
            _response.StatusCode = StatusCodes.Status200OK;
            _service.Atualizar(_request).Returns(_response); 

            var result = (ObjectResult)await _controller.Atualizar(_request);

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void Alterar_Usuario_QuandoFalhar_DeveRetornarInternalServerError()
        {

            _response.StatusCode = StatusCodes.Status500InternalServerError;
            _service.Atualizar(_request).Returns(_response);

            var result = (ObjectResult)await _controller.Atualizar(_request);

            result.StatusCode.Should().Be(500);
        }

        [Fact]
        public async void Login_Usuario_QuandoTudoEstiverCorreto_DeveRetornarSucesso()
        {

            _response.StatusCode = StatusCodes.Status200OK;
            _service.AutenticarUsuario(_requestLogin).Returns(_response);

            var result = (ObjectResult)await _controller.AutenticarUsuario(_requestLogin);

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void Login_Usuario_QuandoFalhar_DeveRetornarBadRequest()
        {

            _response.StatusCode = StatusCodes.Status400BadRequest;
            _service.AutenticarUsuario(_requestLogin).Returns(_response);

            var result = (ObjectResult)await _controller.AutenticarUsuario(_requestLogin);

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async void ObterUsuario_Usuario_QuandoTudoEstiverCorreto_DeveRetornarSucesso()
        {

            _response.StatusCode = StatusCodes.Status200OK;
            _service.ObterPorId(Arg.Any<long>()).Returns(_response);

            var result = (ObjectResult)await _controller.ObterPorId(1);

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void ObterUsuario_Usuario_QuandoFalhar_DeveRetornarBadRequest()
        {

            _response.StatusCode = StatusCodes.Status400BadRequest;
            _service.ObterPorId(Arg.Any<long>()).Returns(_response);

            var result = (ObjectResult)await _controller.ObterPorId(1);

            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async void ObterLista_Usuario_QuandoTudoEstiverCorreto_DeveRetornarSucesso()
        {

            _response.StatusCode = StatusCodes.Status200OK;
            _service.ObterLista().Returns(_response);

            var result = (ObjectResult)await _controller.ObterLista();

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async void ObterLista_Usuario_QuandoFalhar_DeveRetornarBadRequest()
        {

            _response.StatusCode = StatusCodes.Status400BadRequest;
            _service.ObterLista().Returns(_response);

            var result = (ObjectResult)await _controller.ObterLista();

            result.StatusCode.Should().Be(400);
        }
    }
}