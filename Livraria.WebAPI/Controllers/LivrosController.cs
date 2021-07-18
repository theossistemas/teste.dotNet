using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Livraria.WebAPI.DTO;
using Livraria.Services.LivrosServices;
using Livraria.WebAPI.Mapper;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Domain;

namespace Livraria.WebAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {

        private readonly CriarLivro _criarLivro;
        private readonly AlterarLivro _alterarLivro;
        private readonly ExcluirLivro _excluirLivro;
        private readonly ConsultarLivro _consultarLivro;

        public LivrosController(ILivrosRepository livroRepository)
        {
            _criarLivro = new CriarLivro(livroRepository);
            _alterarLivro = new AlterarLivro(livroRepository);
            _excluirLivro = new ExcluirLivro(livroRepository);
            _consultarLivro = new ConsultarLivro(livroRepository);
        }

        [HttpPost("Criar")]
        #region Criar
        public async Task<IActionResult> Criar([FromBody] Livro livro)
        {
            var livroExistente = await _consultarLivro.BuscarPorNome(livro.Nome);
            if (livroExistente != null)
                return NotFound(new { msg = "Livro já cadastrado na biblioteca" });

             await _criarLivro.Executar(livro);

            return Ok(new { msg = "Livro criado com sucesso!" });
        }
        #endregion

        [HttpPut("Alterar/{id}")]
        #region Alterar/{id}
        public async Task<IActionResult> Alterar(int id, [FromBody] LivroDTO livroDTO)
        {
            if (id != livroDTO.Id)
                return NotFound(new { msg = "Livro não encontrada!" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var livro = LivroMapper.ReturnLivro(livroDTO);

            await _alterarLivro.Alterar(id, livro);

            return Ok(new { msg = "Livro alterado com sucesso!" });
        }
        #endregion

        [HttpGet("Buscar-livro/{id}")]
        #region Buscar-Livro/{id}
        public async Task<ActionResult<LivroDTO>> BuscarPorId(int id)
        {
            var livro = await _consultarLivro.BuscarPorId(id);

            if (livro == null)
                return NotFound(new { msg = "Livro não encontrada!" });

            var LivrosDTO = LivroMapper.MapperLivroDTO(livro);

            return LivrosDTO;
        }
        #endregion

        [HttpGet("Listar-Todos")]
        #region Listar-Todos
        public async Task<IEnumerable<LivroDTO>> ListarTodos()
        {

            var livros = await _consultarLivro.ListarTodos();

            var ListaDeLivrosDTO = LivroMapper.MapperListaDeLivrosDTO(livros);

            return ListaDeLivrosDTO;
        }
        #endregion

        [HttpDelete("Excluir")]
        #region Excluir
        public async Task<IActionResult> Excluir(Livro livro)
        {
            var livroA = await _consultarLivro.BuscarPorNome(livro.Nome);

            if (livroA == null)
                return NotFound(new { msg = "Livro não encontrada!" });

            await _excluirLivro.Executar(livroA);

            return Ok(new { msg = "Livro excluído com sucesso!" });
        }
        #endregion
    }

}
