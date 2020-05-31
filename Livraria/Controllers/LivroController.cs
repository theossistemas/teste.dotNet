using Livraria.Application.Interfaces;
using Livraria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Livraria.Controllers
{
    [Route("api/livro")]
    [ApiController]
    [Authorize]
    public class LivroController : ControllerBase
    {
        private readonly ILivroAppService _livroAppService;

        public LivroController(ILivroAppService livroAppService)
        {
            _livroAppService = livroAppService;
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public ResultViewModel Post([FromBody] Livro livro)
        {
            if (livro == null)
                return new ResultViewModel
                {
                    Sucesso = false,
                    Mensagem = "Falha na inclusão do livro"
                };

            _livroAppService.AddLivro(livro);
            return new ResultViewModel
            {
                Sucesso = true,
                Mensagem = "Livro Incluido com sucesso"
            };
        }

        [Route("get-all")]
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Livro> GetAll()
        {
            return _livroAppService.GetAllLivro();
        }

        [Route("{id}")]
        [HttpGet]
        [AllowAnonymous]
        public Livro Get(int id)
        {
            return _livroAppService.GetLivroById(id);
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public ResultViewModel Update([FromBody]Livro livro)
        {
            _livroAppService.UpdateLivro(livro);
            return new ResultViewModel
            {
                Sucesso = true,
                Mensagem = "Livro atualizado com sucesso"
            };
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public ResultViewModel Delete(int id)
        {
            _livroAppService.RemoveLivro(id);
            return new ResultViewModel
            {
                Sucesso = true,
                Mensagem = "Livro removido com sucesso"
            };
        }
    }
}