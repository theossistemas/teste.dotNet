using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using RestAPIClient.Response;
using RestAPIClient.Usuarios;
using System;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private IUsuarioClient usuarioClient;

        public LoginController(IUsuarioClient usuarioClient)
        {
            this.usuarioClient = usuarioClient;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(new UsuarioDTO());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UsuarioDTO usuario)
        {
            IApiResponse response = await usuarioClient.ValidarLogin(usuario.Login, usuario.Senha);

            if (!(response.ResponseBody as Boolean?).GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Usuário e/ou senha inválidos!";

                return View("Index", new UsuarioDTO());
            }

            HttpContext.Session.SetString("User", usuario.Login);

            return View("~/Views/Livro/Livro", new LivroDTO());
        }
    }
}
