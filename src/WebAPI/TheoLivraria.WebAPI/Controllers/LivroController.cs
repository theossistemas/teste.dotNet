using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheoLivraria.Dominio.IRepositories;
using TheoLivraria.Historia.Livros;
using TheoLivraria.WebAPI.Factories;
using TheoLivraria.WebAPI.Models;

namespace TheoLivraria.WebAPI.Controllers
{
    [Route("api/livros")]
    public class LivroController : ControllerBase
    {
        private readonly CriarLivro _criarLivro;
        private readonly AlterarLivro _alterarLivro;
        private readonly ExcluirLivro _excluirLivro;
        private readonly ConsultarLivro _consultarLivro;
        

        public LivroController(ILivroRepository livroRepository)
        {
            _criarLivro = new CriarLivro(livroRepository);
            _alterarLivro = new AlterarLivro(livroRepository);
            _excluirLivro = new ExcluirLivro(livroRepository);
            _consultarLivro = new ConsultarLivro(livroRepository);
            
        }

        [HttpPost("criar")]
        public async Task<IActionResult> Criar([FromBody] LivroViewModel livroViewModel)
        {
            var livroExistente = await _consultarLivro.BuscarPorNome(livroViewModel.Nome);
            if (livroExistente != null)
                return NotFound(new { msg = "Livro já cadastrado na biblioteca" });

            var livro = LivroFactory.MapearLivro(livroViewModel);

            await _criarLivro.Executar(livro);

            return Ok(new { msg = "Livro criado com sucesso" });
        }

        [HttpPut("alterar/{id}")]
        public async Task<IActionResult> Alterar(int id, [FromBody] LivroViewModel livroViewModel)
        {
            if (id != livroViewModel.Id)
                return NotFound(new { msg = "Livro não encontrada" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var livro = LivroFactory.MapearLivro(livroViewModel);

            await _alterarLivro.Executar(id, livro);

            return Ok(new { msg = "Livro alterado com sucesso" });
        }

        [HttpGet("buscar-livro/{id}")]
        public async Task<ActionResult<LivroViewModel>> BuscarPorId(int id)
        {
            var livro = await _consultarLivro.BuscarPorId(id);
            
            if (livro == null)
                return NotFound(new { msg = "Livro não encontrada" });

            var livroViewMovel = LivroFactory.MapearLivroViewModel(livro);

            return livroViewMovel;
        }

        [HttpGet("listar-todos")]
        public async Task<IEnumerable<LivroViewModel>> ListarTodos()
        {

            var livros = await _consultarLivro.ListarTodos();

            var listaLivrosViewMovel = LivroFactory.MapearListaDeLivrosViewModel(livros);

            return listaLivrosViewMovel;
        }

        [HttpDelete("excluir")]
        public async Task<IActionResult> Excluir(int id)
        {
            var livro = await _consultarLivro.BuscarPorId(id);

            if (livro == null)
                return NotFound(new { msg = "Livro não encontrada" });

            await _excluirLivro.Executar(livro);

            return Ok(new { msg = "Livro excluído com sucesso" });
        }
    }
}
