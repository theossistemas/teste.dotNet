
using TheosLivraria.Domain.Entidades;

namespace TheosLivraria.Tests.Domain
{
    public class UsuarioTests
    {
        [Fact]
        public void Criar_Usuario_Valido()
        {
            var usuario = new Usuario(
                nome: "Carlos",
                documento: "12345678900",
                email: "carlos@email.com",
                senha: "senha123",
                telefone: "11999999999",
                dataNascimento: new DateTime(1990, 1, 1),
                perfil: Perfil.Administrador
            );

            Assert.Equal("Carlos", usuario.Nome);
            Assert.Equal("12345678900", usuario.Documento);
            Assert.Equal("carlos@email.com", usuario.Email);
            Assert.NotEqual("senha123", usuario.Senha); // Deve ser hash
            Assert.Equal("11999999999", usuario.Telefone);
            Assert.Equal(new DateTime(1990, 1, 1), usuario.DataNascimento);
            Assert.Equal(Perfil.Administrador, usuario.Perfil);
        }

        [Fact]
        public void Alterar_Dados_Usuario()
        {
            var usuario = new Usuario(
                "Carlos", "123", "carlos@email.com", "senha123", "11999999999", new DateTime(1990, 1, 1), Perfil.Administrador
            );

            usuario.AlterarDados("Novo Nome", "456", "novo@email.com", "11888888888", new DateTime(1991, 2, 2));

            Assert.Equal("Novo Nome", usuario.Nome);
            Assert.Equal("456", usuario.Documento);
            Assert.Equal("novo@email.com", usuario.Email);
            Assert.Equal("11888888888", usuario.Telefone);
            Assert.Equal(new DateTime(1991, 2, 2), usuario.DataNascimento);
            Assert.NotNull(usuario.DataAtualizacao);
        }

        [Fact]
        public void Deve_Alterar_Senha_Usuario()
        {
            var usuario = new Usuario(
                "Carlos", "123", "carlos@email.com", "senha123", "11999999999", new DateTime(1990, 1, 1), Perfil.Administrador
            );

            usuario.AlterarSenha("novaSenha123");
            Assert.NotEqual(Usuario.GerarHash("senha123"), usuario.Senha);
            Assert.Equal(Usuario.GerarHash("novaSenha123"), usuario.Senha);
            Assert.NotNull(usuario.DataAtualizacao);
        }

        [Theory]
        [InlineData("", "123", "email@email.com", "senha123", "11999999999", "Nome é obrigatório.")]
        [InlineData("Carlos", "", "email@email.com", "senha123", "11999999999", "Documento é obrigatório.")]
        [InlineData("Carlos", "123", "email", "senha123", "11999999999", "E-mail inválido.")]
        [InlineData("Carlos", "123", "email@email.com", "123", "11999999999", "Senha deve ter pelo menos 6 caracteres.")]
        [InlineData("Carlos", "123", "email@email.com", "senha123", "", "Telefone é obrigatório.")]
        [InlineData("Carlos", "123", "email@email.com", "senha123", "11999999999", "Data de nascimento inválida.", 2100, 1, 1)]
        public void Nao_Criar_Usuario_Invalido(
            string nome, string documento, string email, string senha, string telefone, string mensagemEsperada,
            int ano = 1990, int mes = 1, int dia = 1)
        {
            var dataNascimento = new DateTime(ano, mes, dia);

            var ex = Assert.Throws<ArgumentException>(() =>
                new Usuario(nome, documento, email, senha, telefone, dataNascimento, Perfil.Administrador)
            );
            Assert.Contains(mensagemEsperada, ex.Message);
        }

        [Fact]
        public void Nao_Alterar_Senha_Invalida()
        {
            var usuario = new Usuario(
                "Carlos", "123", "carlos@email.com", "senha123", "11999999999", new DateTime(1990, 1, 1), Perfil.Administrador
            );

            var ex = Assert.Throws<ArgumentException>(() => usuario.AlterarSenha("123"));
            Assert.Contains("Senha deve ter pelo menos 6 caracteres.", ex.Message);
        }
    }
}
