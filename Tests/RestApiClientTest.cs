using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.DTO;
using RestAPIClient.Factories;
using RestAPIClient.Livros;
using RestAPIClient.Response;
using RestAPIClient.Usuarios;

namespace Tests
{
    [TestClass]
    public class RestApiClientTest
    {
        [TestMethod]
        public void BuscarCliente()
        {
            ILivroClient client = ApiClientFactory.CriarClienteLivro();

            IApiResponse response = client.FindAll().Result;
        }

        [TestMethod]
        public void BuscarClientePorId()
        {
            ILivroClient client = ApiClientFactory.CriarClienteLivro();

            IApiResponse response = client.Find(1L).Result;
        }

        [TestMethod]
        public void CriarLivro()
        {
            ILivroClient client = ApiClientFactory.CriarClienteLivro();

            LivroDTO livro = new LivroDTO { Titulo = "Test Api", Descricao = "Como testar uma API, Volume 1" };

            UsuarioDTO usuario = new UsuarioDTO { Login = "admin", Senha = "1234" };

            IApiResponse response = client.Save(livro, usuario).Result;
        }

        [TestMethod]
        public void AtualizarLivro()
        {
            ILivroClient client = ApiClientFactory.CriarClienteLivro();

            LivroDTO livro = new LivroDTO { Id = 4L, Titulo = "Test Api", Descricao = "Como testar uma API, Volume 1" };

            UsuarioDTO usuario = new UsuarioDTO { Login = "admin", Senha = "1234" };

            IApiResponse response = client.Update(livro.Id, livro, usuario).Result;
        }

        [TestMethod]
        public void TestarValidacaoSenha()
        {
            IUsuarioClient client = ApiClientFactory.CriarClienteUsuario();

            IApiResponse response = client.ValidarLogin("admin", "1234").Result;
        }
    }
}
