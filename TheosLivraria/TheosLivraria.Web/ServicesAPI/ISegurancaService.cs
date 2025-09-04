using Refit;
using TheosLivraria.Web.Request.Seguranca;

using TheosLivraria.Web.Util;

namespace TheosLivraria.Web.ServicesAPI
{
    public interface ISegurancaService
    {
        [Post("/api/v1/seguranca/login")]
        Task<Retorno<string>> Login([Body] LoginRequest request);
    }
}
