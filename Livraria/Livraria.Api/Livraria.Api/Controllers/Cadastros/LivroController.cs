using AutoMapper;
using Livraria.Api.Dto;
using Livraria.Domain.Dto.Cadastros;
using Livraria.Domain.Entities.Cadastros;
using Livraria.Services.Dto;
using Livraria.Services.Interfaces.Cadastros;
using Livraria.Util.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Api.Controllers.Cadastros
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerApiBase
    {
        private readonly IMapper _mapper;
        private readonly ILivroService _livroServico;

        public LivroController(IMapper mapper, ILivroService livroServico, ILogger<LivroController> logger) : base(logger)
        {
            _mapper = mapper;
            _livroServico = livroServico;
        }

        [HttpGet("Consultar/{codigo}")]
        public async Task<ActionResult<LivroDto>> Consultar(int codigo)
        {
            try
            {
                var livro = await _livroServico.ConsultarPorId(codigo);
                var livroDto = _mapper.Map<Livro, LivroDto>(livro);
                return Ok(livroDto);
            }
            catch (AggregateException aex)
            {
                LogarErro($"Erro ao tentar consultar o livro {codigo}: {aex.Message} {aex.InnerExceptions.MontarMensagemErro()}");
                return BadRequest(new ErroResponse(aex.InnerExceptions));
            }

            catch (Exception ex)
            {
                LogarErro($"Erro ao tentar consultar o livro {codigo}: {ex.Message} {ex.InnerException}");
                return BadRequest(new ErroResponse(ex));
            }
        }


        [HttpGet("ConsultarTodosAscendente")]
        public async Task<ActionResult<List<LivroDto>>> ConsultarTodosAscendente()
        {
            try
            {
                var livros = await _livroServico.ConsultarTodosOrderByAsc();
                var livrosDto = _mapper.Map<IList<Livro>, IList<LivroDto>>(livros);

                return Ok(livrosDto);
            }
            catch (AggregateException aex)
            {
                LogarErro($"Erro ao tentar consultar todos os livros: {aex.Message} {aex.InnerExceptions.MontarMensagemErro()}");
                return BadRequest(new ErroResponse(aex.InnerExceptions));
            }

            catch (Exception ex)
            {
                LogarErro($"Erro ao tentar consultar todos os livros: {ex.Message} {ex.InnerException}");
                return BadRequest(new ErroResponse(ex));
            }

        }

        [HttpPost("Cadastrar")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ResponseDto>> Cadastrar([FromBody] LivroDto livroDto)
        {
            try
            {
                LogarInformation($"Solicitação de cadastro de um livro. Autor: {livroDto.Autor}; Gênero: {livroDto.Genero}; Título: {livroDto.Titulo}");
                var livro = _mapper.Map<LivroDto, Livro>(livroDto);
                var response = await _livroServico.InserirNovo(livro);
                return Ok(response);
            }
            catch (AggregateException aex)
            {
                LogarErro($"Erro ao tentar cadastrar um livro. Autor: {livroDto.Autor}; Gênero: {livroDto.Genero}; Título: {livroDto.Titulo}: {aex.Message} {aex.InnerExceptions.MontarMensagemErro()}");
                return BadRequest(new ErroResponse(aex.InnerExceptions));
            }

            catch (Exception ex)
            {
                LogarErro($"Erro ao tentar cadastrar um livro. Autor: {livroDto.Autor}; Gênero: {livroDto.Genero}; Título: {livroDto.Titulo}: {ex.Message} {ex.InnerException}");
                return BadRequest(new ErroResponse(ex));
            }
        }


        [HttpPut("Alterar/{codigo}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ResponseDto>> Alterar(int codigo, [FromBody] LivroDto livroDto)
        {
            try
            {
                LogarWarning($"Solicitação de alteração do livro {livroDto.Codigo}");
                var response = await _livroServico.AlterarLivro(livroDto, codigo);
                return Ok(response);
            }
            catch (AggregateException aex)
            {
                LogarErro($"Erro ao tentar alterar o cadastro de um livro. Código: {livroDto.Codigo}; Autor: {livroDto.Autor}; Gênero: {livroDto.Genero}; Título: {livroDto.Titulo}: {aex.Message} {aex.InnerExceptions.MontarMensagemErro()}");
                return BadRequest(new ErroResponse(aex.InnerExceptions));
            }

            catch (Exception ex)
            {
                LogarErro($"Erro ao tentar alterar o cadastro de um livro. Código: {livroDto.Codigo}; Autor: {livroDto.Autor}; Gênero: {livroDto.Genero}; Título: {livroDto.Titulo}: {ex.Message} {ex.InnerException}");
                return BadRequest(new ErroResponse(ex));
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("Excluir/{codigo}")]
        public async Task<ActionResult<ResponseDto>> Excluir(int codigo)
        {
            try
            {
                LogarWarning($"Solicitação de exclusão do livro {codigo}");
                var response = await _livroServico.ExcluirLivro(codigo);
                return Ok(response);
            }
            catch (AggregateException aex)
            {
                LogarErro($"Erro ao tentar excluir o livro: {codigo}. {aex.Message} {aex.InnerExceptions.MontarMensagemErro()}");
                return BadRequest(new ErroResponse(aex.InnerExceptions));
            }

            catch (Exception ex)
            {
                LogarErro($"Erro ao tentar excluir o livro: {codigo}. {ex.Message} {ex.InnerException}");
                return BadRequest(new ErroResponse(ex));
            }
        }
    }
}
