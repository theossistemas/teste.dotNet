using Livraria.Domain.Dto.Cadastros;
using Livraria.Domain.Entities.Cadastros;
using Livraria.Infra.Data.Interfaces.Repositories.Cadastros;
using Livraria.Services.Dto;
using Livraria.Services.Interfaces.Cadastros;
using Livraria.Util.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Api.Test.ServicesMock.Cadastros
{
    public class LivroServiceMock : ILivroService
    {
        private readonly ILivroRepositorio _livroRepositorio;

        public LivroServiceMock()
        {
        }

        public LivroServiceMock(ILivroRepositorio livroRepositorio)
        {
            _livroRepositorio = livroRepositorio;
        }
        public async Task<ResponseDto> AlterarLivro(LivroDto livroDto, int codigo)
        {
            var livro = await _livroRepositorio.GetById(codigo);
            livro.Autor = livroDto.Autor;
            livro.GeneroId = livroDto.Genero.TryToInt();
            livro.Titulo = livroDto.Titulo;
            _livroRepositorio.Update(livro).RunSynchronously();

            return await Task.FromResult(new ResponseDto());
        }

        public async Task<Livro> ConsultarPorId(int id)
        {
            var livro = new Livro
            {
                Autor = "J. R. R. Tolkien.",
                GeneroId = 4,
                Id = 1,
                Titulo = "O Senhor dos Aneis - A sociedade do anel"
            };

            return await Task.FromResult(livro);
        }

        public async Task<List<Livro>> ConsultarTodosOrderByAsc()
        {
            var livros = new List<Livro>
            {
                new Livro
                {
                    Autor = "J. R. R. Tolkien.",
                    GeneroId = 4,
                    Id = 1,
                    Titulo = "O Senhor dos Aneis - A sociedade do anel"
                },
                 new Livro
                 {
                     Autor = "J. R. R. Tolkien.",
                     GeneroId = 4,
                     Id = 2,
                     Titulo = "O Senhor dos Aneis - As duas torres"
                 },
                 new Livro
                 {
                     Autor = "J. R. R. Tolkien.",
                     GeneroId = 4,
                     Id = 3,
                     Titulo = "O Senhor dos Aneis - O retorno do rei"
                 }

            };

            return await Task.FromResult(livros.OrderBy(x => x.Titulo).ToList());

        }

        public Task<ResponseDto> ExcluirLivro(int codigo)
        {
            _livroRepositorio.Delete(codigo).RunSynchronously();
            return Task.FromResult(new ResponseDto());
        }

        public Task Inserir(Livro livro)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto> InserirNovo(Livro livro)
        {
            _livroRepositorio.Create(livro).RunSynchronously();

            return await Task.FromResult(new ResponseDto());
        }
    }
}
