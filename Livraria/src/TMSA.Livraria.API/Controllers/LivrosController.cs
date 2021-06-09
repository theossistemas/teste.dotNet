using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMSA.Livraria.API.ViewModels;
using TMSA.Livraria.Domain.Interfaces;
using TMSA.Livraria.Domain.Models;

namespace TMSA.Livraria.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Livros")]
    public class LivrosController : MainController
    {
        private readonly ILivroServices _livroServices;
        private readonly IMapper _mapper;

        public LivrosController(ILivroServices livroServices,
                                IMapper mapper,
                                INotificador notificador) : base(notificador)
        {
            _livroServices = livroServices;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<LivroViewModel>> ObterLivros()
        {
            return _mapper.Map<IEnumerable<LivroViewModel>>(await _livroServices.ObterLivros());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<LivroViewModel>> ObterLivroPorId(Guid id)
        {
            var livro = _mapper.Map<LivroViewModel>(await _livroServices.ObterLivroPorId(id));

            if (livro == null) return NotFound();

            return livro;
        }

        [HttpPost]
        public async Task<ActionResult<LivroViewModel>> CriarLivro([FromBody] LivroViewModel livroViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _livroServices.CriarLivro(_mapper.Map<Livro>(livroViewModel));

            return CustomResponse(livroViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<LivroViewModel>> AtualizarLivro(Guid id, [FromBody] LivroViewModel livroViewModel)
        {
            if (id != livroViewModel.LivroId)
            {
                NotificarErro("O id informado não é o mesmo que foi passado");
                return CustomResponse(livroViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _livroServices.AtualizarLivro(_mapper.Map<Livro>(livroViewModel));

            return CustomResponse(livroViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<LivroViewModel>> RemoverLivro(Guid id)
        {
            var livroViewModel = _mapper.Map<LivroViewModel>(await _livroServices.ObterLivroPorId(id));

            if (livroViewModel == null) return NotFound();

            await _livroServices.RemoverLivro(id);

            return CustomResponse(livroViewModel);
        }
    }
}
