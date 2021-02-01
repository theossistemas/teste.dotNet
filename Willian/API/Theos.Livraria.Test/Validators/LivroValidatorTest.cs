using AutoFixture;
using FluentAssertions;
using Theos.Livraria.Application.Validators;
using Theos.Livraria.Domain.Model.Livro;
using Xunit;

namespace Theos.Livraria.Test.Validators
{
    public class LivroValidatorTest
    {
        private readonly Fixture _fixture;
        private readonly LivroValidator _validator;
        private readonly RequestLivro _request;

        public LivroValidatorTest() 
        {
            _fixture = new Fixture();
            _validator = new LivroValidator();
            _request = _fixture.Create<RequestLivro>();
        }

        [Fact]
        public void UsuarioRequest_QuandoTituloForNulo_DeveRetornarErro()
        {
            _request.Titulo = null;
            var resultado = _validator.Validate(_request);
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void UsuarioRequest_QuandoAutorForNulo_DeveRetornarErro()
        {
            _request.Autor = null;
            var resultado = _validator.Validate(_request);
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void UsuarioRequest_QuandoIdUsuarioForNulo_DeveRetornarErro()
        {
            _request.IdUsuario = 0;
            var resultado = _validator.Validate(_request);
            resultado.IsValid.Should().BeFalse();
        }
    }
}