using AutoMapper;

using Livraria.Domain.Contexto;
using Livraria.Domain.Livros;
using Livraria.Domain.ManyToMany;
using Livraria.Domain.Pessoas;
using Livraria.Web.Models.Livros;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Web.Api
{
    [Route("api/livro")]
    public class LivroController : BaseApiController
    {
        private readonly IContextoDeDados _contexto;
        private readonly IMapper _mapper;

        public LivroController(IContextoDeDados contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }


        [HttpGet, Route("{take}/{skip}"), AllowAnonymous]
        public ActionResult BuscarLivros(int take, int skip, string busca, string tema)
        {
            IQueryable<Livro> livros = _contexto.Livros;

            livros = livros.OrderBy(l => l.Titulo);

            if (!string.IsNullOrEmpty(busca) && !string.IsNullOrWhiteSpace(busca))
                livros = livros.Where(l =>
                                                l.Autores.Any(a => a.Autor.Nome.Contains(busca)) ||
                                                l.Titulo.Contains(busca)
                                                );


            if (!string.IsNullOrEmpty(tema) && !string.IsNullOrWhiteSpace(tema))
            {
                livros = livros.Where(l => l.Temas.Any(t => t.Tema.Valor == tema));
            }

            List<LivroModel> livrosModel = new List<LivroModel>();
            var quantidadeLivros = livros.Count();
            livros = livros.Take(take).Skip(skip);

            foreach (Livro livro in livros)
            {
                var autores = _contexto.AutoresLivros.Where(al => al.IdLivro == livro.Id).ToList();
                foreach (var autorLivro in autores)
                {
                    autorLivro.Autor = _contexto.Pessoas.Find(autorLivro.IdAutor);
                    livro.Autores.Add(autorLivro);
                }

                List<LivroTema> temas = _contexto.LivrosTemas.Where(al => al.IdLivro == livro.Id).ToList();
                foreach (var livroTema in temas)
                {
                    livroTema.Tema = _contexto.Temas.Find(livroTema.IdTema);
                    livro.Temas.Add(livroTema);
                }

                LivroModel model = _mapper.Map<LivroModel>(livro);
                livrosModel.Add(model);
            }


            return Ok(new { livrosModel, quantidadeLivros });
        }

        [HttpPut]
        public async Task<ActionResult> AtualizarLivro([FromBody] LivroModel livroModel)
        {
            Livro livro = _mapper.Map<Livro>(livroModel);

            if (_contexto.Livros.Any(l => l.Titulo == livroModel.Titulo && l.Id != livroModel.Id))
                return BadRequest("Livro ja cadastrado.");

            if (livro.Id > 0)
                _contexto.Livros.Update(livro);
            else
                _contexto.Livros.Add(livro);

            _contexto.SaveChanges();

            livro.Temas = _contexto.LivrosTemas.Where(lt => lt.IdLivro == livro.Id).ToList();

            livro = await AtualizarTemas(livroModel, livro);

            livro.Autores = _contexto.AutoresLivros.Where(ul => ul.IdLivro == livro.Id).ToList();

            livro = await AtualizarAutores(livroModel, livro);

            //_contexto.SaveChanges();

            return Ok(_mapper.Map<LivroModel>(livro));
        }

        [HttpDelete, Route("{id}")]
        public ActionResult DeletarLivro(int id)
        {
            Livro livro = _contexto.Livros.Find(id);

            IQueryable<AutorLivro> relacaoAutores = _contexto.AutoresLivros.Where(al => al.IdLivro == id);
            _contexto.Remove(relacaoAutores);

            IQueryable<LivroTema> relacaoTemas = _contexto.LivrosTemas.Where(al => al.IdLivro == id);
            _contexto.Remove(relacaoTemas);

            _contexto.Remove(livro);
            _contexto.SaveChanges();

            return Ok();
        }

        private Task<Livro> AtualizarAutores(LivroModel livroModel, Livro livro)
        {
            foreach (Models.Pessoas.PessoaModel autor in livroModel.Autores)
            {
                var autorExistente = _contexto.Pessoas.FirstOrDefault(a => a.Nome == autor.Nome);
                if (autorExistente != null)
                {
                    if (livro.Autores != null && !livro.Autores.Any(t => t.Autor == autorExistente))
                    {
                        var livroAutor = new AutorLivro { IdAutor = autorExistente.Id, Autor = autorExistente, Livro = livro, IdLivro = livro.Id };

                        livro.Autores.Add(livroAutor);
                        _contexto.SaveChanges();
                    }
                }
                else
                {
                    Pessoa pessoaAutor = new Pessoa { Nome = autor.Nome };
                    _contexto.Add(pessoaAutor);
                    _contexto.SaveChanges();

                    AutorLivro autorLivro = new AutorLivro { Autor = pessoaAutor, IdAutor = pessoaAutor.Id, IdLivro = livro.Id, Livro = livro };
                    _contexto.Add(autorLivro);
                    _contexto.SaveChanges();
                    livro.Autores.Add(autorLivro);
                    _contexto.SaveChanges();
                }
            }

            return Task.FromResult(livro);
        }

        private Task<Livro> AtualizarTemas(LivroModel livroModel, Livro livro)
        {
            foreach (string tema in livroModel.Temas)
            {
                var temaExistente = _contexto.Temas.FirstOrDefault(t => t.Valor == tema);
                if (temaExistente != null)
                {
                    if (livro.Temas != null && !livro.Temas.Any(t => t.Tema == temaExistente))
                    {
                        var livroTema = new LivroTema { IdTema = temaExistente.Id, Tema = temaExistente, Livro = livro, IdLivro = livro.Id };

                        livro.Temas.Add(livroTema);
                        _contexto.SaveChanges();
                    }
                }
                else
                {
                    Tema novoTema = new Tema { Valor = tema };
                    _contexto.Add(novoTema);
                    _contexto.SaveChanges();

                    LivroTema livroTema = new LivroTema { Tema = novoTema, IdTema = novoTema.Id, IdLivro = livro.Id, Livro = livro };
                    _contexto.Add(livroTema);
                    _contexto.SaveChanges();
                    livro.Temas.Add(livroTema);
                    _contexto.SaveChanges();
                }
            }

            return Task.FromResult(livro);
        }
    }
}
