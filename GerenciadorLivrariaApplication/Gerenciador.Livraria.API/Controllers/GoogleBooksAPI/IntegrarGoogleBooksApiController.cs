using AutoMapper;
using Gerenciador.Livraria.Core.Entities;
using Gerenciador.Livraria.Core.Helpers.GoogleBooks;
using Gerenciador.Livraria.Core.Interfaces.ServicesInterface;
using Gerenciador.Livraria.DTOs.DTOs.GoogleBooks;
using Gerenciador.Livraria.DTOs.DTOs.Livros;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Gerenciador.Livraria.Core.Services.GoogleBooksAPI.GoogleBooksService;

namespace Gerenciador.Livraria.API.Controllers.GoogleBooksAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrarGoogleBooksApiController : ControllerBase
    {
        private readonly IGoogleBooksService _googleBooksService;

        public IntegrarGoogleBooksApiController(IGoogleBooksService googleBooksService)
        {
            _googleBooksService = googleBooksService;
        }

        [AllowAnonymous]
        [HttpGet("pesquisar/titulo/{titulo}")]
        public async Task<IActionResult> PesquisarPeloTitulo(string titulo)
        {
            var consultaNoGoogleBooks = await _googleBooksService.BuscarObraPeloTitulo(titulo);

            if (consultaNoGoogleBooks.CodigoDeStatus != System.Net.HttpStatusCode.OK)
                return StatusCode((int)consultaNoGoogleBooks.CodigoDeStatus, consultaNoGoogleBooks.Mensagem);

            var resultadoDaConsulta = GoogleBooksMapper.MapearDadosDaObraEmJson(consultaNoGoogleBooks.Dados);

            if (resultadoDaConsulta is null || !resultadoDaConsulta.Any())
                return NotFound();

            return Ok(resultadoDaConsulta);
        }

        [AllowAnonymous]
        [HttpGet("pesquisar/autor/{autor}")]
        public async Task<IActionResult> PesquisarObrasDoAutor(string autor)
        {
            var consultaNoGoogleBooks = await _googleBooksService.BuscarObrasDoAutor(autor);

            if (consultaNoGoogleBooks.CodigoDeStatus != System.Net.HttpStatusCode.OK)
                return StatusCode((int)consultaNoGoogleBooks.CodigoDeStatus, consultaNoGoogleBooks.Mensagem);

            var resultadoDaConsulta = GoogleBooksMapper.MapearDadosDaObraEmJson(consultaNoGoogleBooks.Dados);

            if (resultadoDaConsulta is null || !resultadoDaConsulta.Any())
                return NotFound();

            return Ok(resultadoDaConsulta);
        }

        [AllowAnonymous]
        [HttpGet("pesquisar/categoria/{categoria}")]
        public async Task<IActionResult> PesquisarPorCategoria(string categoria)
        {
            var consultaNoGoogleBooks = await _googleBooksService.BuscarObrasPorCategoria(categoria);

            if (consultaNoGoogleBooks.CodigoDeStatus != System.Net.HttpStatusCode.OK)
                return StatusCode((int)consultaNoGoogleBooks.CodigoDeStatus, consultaNoGoogleBooks.Mensagem);

            var resultadoDaConsulta = GoogleBooksMapper.MapearDadosDaObraEmJson(consultaNoGoogleBooks.Dados);

            if (resultadoDaConsulta is null || !resultadoDaConsulta.Any())
                return NotFound();

            return Ok(resultadoDaConsulta);
        }
    }
}
