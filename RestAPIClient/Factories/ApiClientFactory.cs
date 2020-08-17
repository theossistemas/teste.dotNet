using RestAPIClient.Livros;
using RestAPIClient.Usuarios;

namespace RestAPIClient.Factories
{
    public class ApiClientFactory
    {
        private ApiClientFactory() { }

        private static ILivroClient livrosClient;

        private static IUsuarioClient usuariosClient;

        public static ILivroClient CriarClienteLivro()
        {
            return livrosClient ??
                (livrosClient = new LivroClient());
        }

        public static IUsuarioClient CriarClienteUsuario()
        {
            return usuariosClient ??
                (usuariosClient = new UsuarioClient());
        }
    }
}
