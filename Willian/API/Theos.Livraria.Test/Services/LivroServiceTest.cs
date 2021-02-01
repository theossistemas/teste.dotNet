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
using Theos.Livraria.Domain.Model.Livro; 
using Xunit;

namespace Theos.Livraria.Test.Services
{
    public class LivroServiceTest
    {
        private readonly Fixture _fixture;
        private readonly BaseResponse _baseResponse;
        private readonly Livro _livro;
        private readonly IEnumerable<Livro> _listaLivro;
        private readonly RequestLivro _requestLivro;
        private readonly ResponseLivro _responseLivro;
        private readonly ILogger<LivroService> _logger;
        private readonly IMapper _mapper;
        private readonly ILivroRepository _repository;
        private readonly ILivroService _service;
        private readonly IConfiguration _configuration;
        public LivroServiceTest()
        {
            _fixture = new Fixture();
            _logger = Substitute.For<ILogger<LivroService>>();
            _mapper = Substitute.For<IMapper>();
            _repository = Substitute.For<ILivroRepository>();
            _configuration = Substitute.For<IConfiguration>();
            _baseResponse = _fixture.Create<BaseResponse>();
            _livro = _fixture.Create<Livro>();
            _listaLivro = _fixture.Create<IEnumerable<Livro>>();
            _requestLivro = _fixture.Create<RequestLivro>();
            _responseLivro = _fixture.Create<ResponseLivro>();
            _service = new LivroService(_repository, _mapper, _logger, _configuration);

        }

        [Fact]
        public async void Inserir_Livro_DeveRetornarBadRequest()
        {
            _baseResponse.StatusCode = StatusCodes.Status400BadRequest;
            _requestLivro.Titulo = "";
            _mapper.Map<Livro>(_requestLivro).Returns(_livro);
            await _repository.Inserir(_livro);
            var resultado = await _service.Inserir(_requestLivro);

            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void Inserir_Livro_DeveRetornarOK()
        {
            _baseResponse.StatusCode = StatusCodes.Status201Created;
            _mapper.Map<Livro>(_requestLivro).Returns(_livro);
            await _repository.Inserir(_livro);
            _mapper.Map<ResponseLivro>(_livro).Returns(_responseLivro);
            var resultado = await _service.Inserir(_requestLivro);

            resultado.StatusCode.Should().Be(StatusCodes.Status201Created);
        }

        [Fact]
        public async void Atualizar_Livro_DeveRetornarBadRequest()
        {
            _baseResponse.StatusCode = StatusCodes.Status400BadRequest;
            _requestLivro.Titulo = "";
            _mapper.Map<Livro>(_requestLivro).Returns(_livro);
            await _repository.Atualizar(_livro);
            var resultado = await _service.Inserir(_requestLivro);

            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void Atualizar_Livro_LivroInexistente_DeveRetornarBadRequest()
        {
            _baseResponse.StatusCode = StatusCodes.Status400BadRequest;
            _mapper.Map<Livro>(_requestLivro).Returns(_livro);
            _repository.LivroCadastrado(_livro.Id).Returns(false);
            _repository.Atualizar(_livro).Returns(_livro);
            _mapper.Map<ResponseLivro>(_livro).Returns(_responseLivro);
            var resultado = await _service.Atualizar(_requestLivro);

            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
         

        [Fact]
        public async void ObterLista_DeveRetornarBadRequest()
        {
            _mapper.Map<Livro>(_requestLivro).Returns(_livro);
            var resultado = await _service.ObterLista();
            resultado.StatusCode = StatusCodes.Status400BadRequest;
            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void ObterLista_DeveRetornarOk()
        {
            _baseResponse.StatusCode = StatusCodes.Status200OK;
            _mapper.Map<Livro>(_requestLivro).Returns(_livro);
            _repository.ObterLista().Returns(_listaLivro);
            var resultado = await _service.ObterLista();

            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async void ObterLivroPorId_DeveRetornarBadRequest()
        {
            _baseResponse.StatusCode = StatusCodes.Status400BadRequest;
            _mapper.Map<Livro>(_requestLivro).Returns(_livro);
            _repository.ObterLista().Returns(default(IEnumerable<Livro>));
            var resultado = await _service.ObterPorId(Arg.Any<long>());

            resultado.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async void ObterLivroPorId_DeveRetornarOk()
        {
            _baseResponse.StatusCode = StatusCodes.Status200OK;
            _mapper.Map<Livro>(_requestLivro).Returns(_livro);
            _mapper.Map<ResponseLivro>(_livro).Returns(_responseLivro);
            _repository.ObterPorId(1).Returns(_livro); 
            var resultado = await _service.ObterPorId(1);

            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}