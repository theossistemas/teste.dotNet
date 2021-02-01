using AutoFixture;
using FluentAssertions;
using Theos.Livraria.Application.Validators;
using Theos.Livraria.Domain.Model.Usuario;
using Xunit;

namespace Theos.Livraria.Test.Validators
{
    public class UsuarioValidatorTest  
    {
        private readonly Fixture _fixture;
        private readonly UsuarioValidator _validator;
        private readonly RequestUsuario _request;

        public UsuarioValidatorTest() 
        {
            _fixture = new Fixture();
            _validator = new UsuarioValidator();
            _request = _fixture.Create<RequestUsuario>();
        }

        [Fact]
        public void UsuarioRequest_QuandoNomeForNulo_DeveRetornarErro()
        {
            _request.Nome = null;
            var resultado = _validator.Validate(_request);
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void UsuarioRequest_QuandoEmailForNulo_DeveRetornarErro()
        {
            _request.Email = null;
            var resultado = _validator.Validate(_request);
            resultado.IsValid.Should().BeFalse();
        }
    }
}