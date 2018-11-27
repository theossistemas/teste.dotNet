using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using theos.livros.Business;
using theos.livros.Entitys;

namespace theos.livros.Controllers
{
    [Route("api/livros")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly LivroBusiness _livroBusiness = new LivroBusiness();

        [HttpGet]
        public async Task<ActionResult<List<Livro>>> Get()
        {
            try
            {
                var _livros = await Task.Run(() => _livroBusiness.ListarLivros());

                return _livros;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Livro livro)
        {
            try
            {
                var _inseriu = _livroBusiness.Inserir(livro);

                if (!_inseriu)
                {
                    return Content("Livro já se encontra cadastrado.");
                }
                else
                {
                    return Ok("Livro inserido com sucesso.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Livro livro)
        {
            try
            {
                var _atualizou = _livroBusiness.Atualizar(livro);

                if (!_atualizou)
                {
                    return Content("Livro não encontrado.");
                }
                else
                {
                    return Ok("Livro atualizado com sucesso.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var _removeu = _livroBusiness.Remover(id);

                if (!_removeu)
                {
                    return Content("Livro não encontrado.");
                }
                else
                {
                    return Ok("Livro excluído com sucesso.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
