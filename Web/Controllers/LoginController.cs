using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using RestAPIClient.Response;
using RestAPIClient.Usuarios;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Models;

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

            ClaimsIdentity identity = new ClaimsIdentity("Administrator");

            identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Login));

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("Administrator", principal);

            Sessao.Usuario = usuario;

            HttpContext.Session.SetString("User", usuario.Login);

            return View("~/Views/Livro/Livro", new LivroDTO());
        }
    }
}
