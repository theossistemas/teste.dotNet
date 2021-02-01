using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Theos.Livraria.Api.Controllers;
using Theos.Livraria.Domain.Interface.Services;
using Theos.Livraria.Domain.Model;
using Theos.Livraria.Domain.Model.Livro; 
using Xunit;

namespace Theos.Livraria.Test.Controllers
{
    public class LivroControllerTest
    {
        private readonly Fixture _fixture;
        private readonly RequestLivro  _request; 
        private readonly BaseResponse _response;
        private readonly ILivroService _service;
        private readonly LivroController _controller;
        private readonly ILogger<LivroController> _logger;

        public LivroControllerTest()  
        {

            _fixture = new Fixture();
           
            _request = _fixture.Create<RequestLivro>(); 
            _response = _fixture.Create<BaseResponse>();
            _service = Substitute.For<ILivroService>();
            _logger = Substitute.For<ILogger<LivroController>>();

            _controller = new LivroController(_service, _logger);
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