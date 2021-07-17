using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Livraria.WebAPI.DTO;
using Livraria.WebAPI.Mapper;
using Livraria.Domain.Interfaces;
using Livraria.Services.LivrosServices;

namespace Livraria.WebAPI.Controllers
{
    [Route("api/livros")]
    public class LivroController : ControllerBase
    {
        private readonly CriarLivro _criarLivro;
        private readonly AlterarLivro _alterarLivro;
        private readonly ExcluirLivro _excluirLivro;
        private readonly ConsultarLivro _consultarLivro;

        public LivroController(ILivrosRepository livrosRepository)
        {
            _criarLivro = new CriarLivro(livrosRepository);
            _alterarLivro = new AlterarLivro(livrosRepository);
            _excluirLivro = new ExcluirLivro(livrosRepository);
            _consultarLivro = new ConsultarLivro(livrosRepository);
        }

        [HttpPost("criar")]
        public async Task<IActionResult> Criar([FromBody] LivroDTO livroDTO)
        {
            var livroExistente = await _consultarLivro.BuscarPorNome(livroDTO.Nome);
            if (livroExistente != null)
                return NotFound(new { msg = "Livro já cadastrado na biblioteca" });

            var livro = LivroFactory.MapearLivro(livroDTO);

            await _criarLivro.Executar(livro);

            return Ok(new { msg = "Livro criado com sucesso" });
        }

        [HttpPut("alterar/{id}")]
        public async Task<IActionResult> Alterar(int id, [FromBody] LivroDTO livroDTO)
        {
            if (id != livroDTO.Id)
                return NotFound(new { msg = "Livro não encontrada" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var livro = LivroFactory.MapearLivro(livroDTO);

            await _alterarLivro.Executar(id, livro);

            return Ok(new { msg = "Livro alterado com sucesso" });
        }

        [HttpGet("buscar-livro/{id}")]
        public async Task<ActionResult<LivroDTO>> BuscarPorId(int id)
        {
            var livro = await _consultarLivro.BuscarPorId(id);

            if (livro == null)
                return NotFound(new { msg = "Livro não encontrada" });

            var livroViewMovel = LivroFactory.MapearLivroDTO(livro);

            return livroViewMovel;
        }

        [HttpGet("listar-todos")]
        public async Task<IEnumerable<LivroDTO>> ListarTodos()
        {

            var livros = await _consultarLivro.ListarTodos();

            var listaLivrosViewMovel = LivroFactory.MapearListaDeLivrosDTO(livros);

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
