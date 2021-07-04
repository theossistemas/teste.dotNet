using Livraria.Domain.Dto.Cadastros;
using Livraria.Domain.Entities.Cadastros;
using Livraria.Infra.Data.Interfaces.Repositories.Cadastros;
using Livraria.Services.Dto;
using Livraria.Services.Interfaces.Cadastros;
using Livraria.Util.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Services.Cadastros
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepositorio _livroRepositorio;

        public LivroService(ILivroRepositorio livroRepositorio)
        {
            _livroRepositorio = livroRepositorio;
        }

        public async Task<Livro> ConsultarPorId(int id)
        {
            return await _livroRepositorio.GetById(id);
        }

        public async Task<List<Livro>> ConsultarTodosOrderByAsc()
        {
            return await _livroRepositorio.GetAll().OrderBy(x => x.Titulo).ToListAsync();
        }

        public async Task Inserir(Livro livro)
        {
            await _livroRepositorio.Create(livro);
        }

        public async Task<ResponseDto> InserirNovo(Livro livro)
        {
            if (!await VerificarLivroJaCadastrado(livro.Titulo, livro.GeneroId))
            {
                await _livroRepositorio.Create(livro);
                return LivroServiceRespostas.ResponderLivroCadastradoComSucesso();
            }
            return LivroServiceRespostas.ResponderLivroJaCadastrado();
        }

        public async Task<ResponseDto> ExcluirLivro(int codigo)
        {
            var livro = await _livroRepositorio.GetById(codigo);
            if (VerificarLivroEncontrado(livro))
            {
                await _livroRepositorio.Delete(livro);
                return LivroServiceRespostas.ResponderLivroExcluidoComSucesso();
            }
            return LivroServiceRespostas.ResponderLivroNaoEncntrado();
        }

        private async Task<bool> VerificarLivroJaCadastrado(string titulo, int generoId)
        {
            var livro = await _livroRepositorio.GetByTituloEGenero(titulo, generoId);
            return livro != null;
        }

        public async Task<ResponseDto> AlterarLivro(LivroDto livroDto, int codigo)
        {
            var livro = await _livroRepositorio.GetById(codigo);
            if (VerificarLivroEncontrado(livro))
            {
                await AlterarLivro(livroDto, livro);
                return LivroServiceRespostas.ResponderLivroAlteradoComSucesso();
            }
            return LivroServiceRespostas.ResponderLivroNaoEncntrado();
        }

        private async Task AlterarLivro(LivroDto livroDto, Livro livro)
        {
            livro.Autor = livroDto.Autor;
            livro.GeneroId = livroDto.Genero.TryToInt();
            livro.Titulo = livroDto.Titulo;
            await _livroRepositorio.Update(livro);
        }

        private bool VerificarLivroEncontrado(Livro livro)
        {
            return livro != null;
        }
    }
}
