using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Theos.Model.Model;
using Theos.Service.Interface;

namespace Theos.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LivroController : ControllerBase
    {

        private ILivroService _livroService;
        private ILogAtividadeService _logAtividadeService;

        public LivroController(ILivroService livroService, ILogAtividadeService logAtividadeService)
        {
            _livroService = livroService;
            _logAtividadeService = logAtividadeService;
        }

        [HttpPost]
        [Authorize]
        public string CadastrarNovoLivro(string nomeLivro)
        {
            _logAtividadeService.GravarAtividade(System.Reflection.MethodBase.GetCurrentMethod().Name);
            var livroJaExiste = _livroService.LivroJaExiste(nomeLivro);
            if (livroJaExiste == true)
            {
                return "O livro já está cadastrado";
            }
            else
            {
                return _livroService.NovoLivro(nomeLivro);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Livro> Get()
        {
            _logAtividadeService.GravarAtividade(System.Reflection.MethodBase.GetCurrentMethod().Name);
            IEnumerable<Livro> livros = _livroService.BuscarLivros();
            return livros.OrderBy(x => x.NomeLivro);
        }
        [HttpPut]
        [Authorize]
        public string AtualizarLivro(int id, string nomeLivro)
        {
            _logAtividadeService.GravarAtividade(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return _livroService.AtualizarLivro(id, nomeLivro);
        }

        [HttpDelete]
        [Authorize]
        public string DeletarLivro(int id)
        {
            _logAtividadeService.GravarAtividade(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return _livroService.ApagarLivro(id);
        }
    }
}
