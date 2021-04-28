using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Theos.Service.Interface;

namespace Theos.Api.Controllers
{
    [ApiController]
    [Route("[controller]"),]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioService _iUsuarioService;
        private ILogAtividadeService _iLogAtividadeService;
        public UsuarioController(IUsuarioService usuarioService, ILogAtividadeService iLogAtividadeService)
        {
            _iUsuarioService = usuarioService;
            _iLogAtividadeService = iLogAtividadeService;
        }

        [HttpPost]
        [AllowAnonymous]
        public string CriarNovoUsuario(string nome, string senha, string senhaRepetida)
        {
            _iLogAtividadeService.GravarAtividade(System.Reflection.MethodBase.GetCurrentMethod().Name);
            //_logAtividadeRepository.AddLogAtividade(System.Reflection.MethodBase.GetCurrentMethod().Name);
            if (!senha.Trim().ToUpper().Equals(senhaRepetida.Trim().ToUpper()))
            {
                return "As senhas não conferem";
            }
            //var senhaCodificada = BCrypt.Net.BCrypt.HashPassword(senha);
            return _iUsuarioService.NovoUsuario(nome, senha);
        }

    }
}
