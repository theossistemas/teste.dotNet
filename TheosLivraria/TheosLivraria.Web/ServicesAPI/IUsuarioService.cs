using Microsoft.AspNetCore.Mvc;
using Refit;
using TheosLivraria.Web.Request.Usuarios;
using TheosLivraria.Web.Util;
using TheosLivraria.Web.Util.Response.Ususarios;

namespace TheosLivraria.Web.ServicesAPI
{
    public interface IUsuarioService
    {
        [Post("/api/v1/usuarios")]
        Task<Retorno<int>> Criar([Body]CriarUsusarioRequest request);

        [Get("/api/v1/usuarios")]
        Task<Retorno<List<UsuariosGridDTO>>> ObterTodos();

        [Get("/api/v1/usuarios/{id}")]
        Task<Retorno<UsuarioDTO>> ObterPorId(Guid Id);

        [Put("/api/v1/usuarios/{id}")]
        Task<Retorno<bool>> Alterar(string id, [Body] AlterarUsuarioRequest request);

        [Delete("/api/v1/usuarios/{id}")]
        Task<Retorno<bool>> Delete(Guid Id);
    }
}
