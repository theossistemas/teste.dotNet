using AutoFixture;
using FluentAssertions;
using TheoLib.CoreAplicacao.Validadores;
using TheoLib.Dominio.Modelo.UsuarioModelo;
using Xunit;

namespace TheoLib.Teste.Validadores
{
    public class UsuarioValidatorTest
    {
        private readonly Fixture _fixture;
        private readonly UsuarioValidador _validator;
        private readonly RequisicaoUsuario _request;

        public UsuarioValidatorTest() 
        {
            _fixture = new Fixture();
            _validator = new UsuarioValidador();
            _request = _fixture.Create<RequisicaoUsuario>();
        }

        [Fact]
        public void NomeNuloErro()
        {
            _request.Nome = null;
            var resultado = _validator.Validate(_request);
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void EmailNuloErro()
        {
            _request.Email = null;
            var resultado = _validator.Validate(_request);
            resultado.IsValid.Should().BeFalse();
        }
    }
}
