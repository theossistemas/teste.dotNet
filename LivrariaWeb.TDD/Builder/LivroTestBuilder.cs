using LivrariaWeb.Domain.Model;
using LivrariaWeb.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace LivrariaWeb.TDD.Builder
{
    public class LivroTestBuilder
    {
        private DtoLivro _livroDto;
        private Livro    _livro;
        public LivroTestBuilder()
        {
            _livroDto = new DtoLivro();
            _livro = new Livro();

        }
        public LivroTestBuilder ComLivro(string nomeLivro, string nomeAutor)
        {
            _livroDto.NomeLivro = nomeLivro;
            _livroDto.NomeAutor = nomeAutor;
            return this;
        }
        public LivroTestBuilder ComLivro(string nomeLivro, string nomeAutor, DateTime dataPublicacao, int numeroPaginas, long id)
        {
            _livro.NomeLivro = nomeLivro;
            _livro.NomeAutor = nomeAutor;
            _livro.DataPublicacao = dataPublicacao;
            _livro.NumeroPaginas = numeroPaginas;
            _livro.Id = id;
            return this;            
        }
        public LivroTestBuilder ComLivroDto(string nomeLivro, string nomeAutor, DateTime dataPublicacao, int numeroPaginas, long id)
        {
            _livroDto.NomeLivro = nomeLivro;
            _livroDto.NomeAutor = nomeAutor;
            _livroDto.DataPublicacao = dataPublicacao;
            _livroDto.NumeroPaginas = numeroPaginas;
            _livroDto.Id = id;
            return this;
        }

        public DtoLivro BuildDto()
        {
            return _livroDto;
        }
        public Livro BuildLivro()
        {
            return _livro;
        }
        public Livro BuildLivroEmpty()
        {
            return _livro = null;
        }
    }
}
