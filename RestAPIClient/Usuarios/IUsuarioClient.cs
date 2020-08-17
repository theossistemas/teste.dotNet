using RestAPIClient.Response;
using System;
using System.Threading.Tasks;

namespace RestAPIClient.Usuarios
{
    public interface IUsuarioClient
    {
        Task<IApiResponse> ValidarLogin(String login, String senha);
    }
}
