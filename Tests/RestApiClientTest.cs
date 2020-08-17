using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void TestarValidacaoSenha()
        {
            IUsuarioClient client = ApiClientFactory.CriarClienteUsuario();

            IApiResponse response = client.ValidarLogin("admin", "1234").Result;
        }
    }
}
