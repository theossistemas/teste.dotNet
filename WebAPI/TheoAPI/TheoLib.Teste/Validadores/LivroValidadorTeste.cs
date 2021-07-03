using AutoFixture;
using FluentAssertions;
using TheoLib.CoreAplicacao.Validadores;
using TheoLib.Dominio.Modelo.LivroModelo;
using Xunit;

namespace TheoLib.Teste.Validadores
{
    public class LivroValidadorTeste
    {

        private readonly Fixture _fixture;
        private readonly LivroValidador _validador;
        private readonly RequisicaoLivro _request;

        public LivroValidadorTeste() 
        {
            _fixture = new Fixture();
            _validador = new LivroValidador();
            _request = _fixture.Create<RequisicaoLivro>();
        }

        [Fact]
        public void TituloNuloErro()
        {
            _request.Titulo = null;
            var resultado = _validador.Validate(_request);
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void AutorrNuloErro()
        {
            _request.Autor = null;
            var resultado = _validador.Validate(_request);
            resultado.IsValid.Should().BeFalse();
        }

        [Fact]
        public void IdUsuarioNuloErro()
        {
            _request.IdUsuario = 0;
            var resultado = _validador.Validate(_request);
            resultado.IsValid.Should().BeFalse();
        }

    }
}
