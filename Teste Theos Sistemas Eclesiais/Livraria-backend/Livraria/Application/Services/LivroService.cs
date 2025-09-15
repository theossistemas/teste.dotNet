using Livraria.Aplicacao.Interfaces;
using Livraria.Domain.Interfaces;
using Livraria.Exceptions;
using Serilog;

namespace Livraria.Application.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _repository;

        public LivroService(ILivroRepository repository)
        {
            _repository = repository;
            Log.Information("LivroService iniciado");
        }

        public async Task<Livro> CriarLivroAsync(Livro livro)
        {
            Log.Information("Criando livro {Titulo} do autor {Autor}", livro.Titulo, livro.Autor);
            var existente = await _repository.ListarAsync();
            if (existente.Any(l => l.Titulo.Equals(livro.Titulo, StringComparison.OrdinalIgnoreCase)) && existente.Any(l => l.Autor.Equals(livro.Autor, StringComparison.OrdinalIgnoreCase)))
            {
                Log.Warning("Livro {Titulo} do autor {Autor} já cadastrado", livro.Titulo, livro.Autor);
                throw new LivroJaCadastradoExcepton(livro.Titulo);
            }

            var result = await _repository.AdicionarAsync(livro);
            Log.Information("Livro {Titulo} criado com sucesso", livro.Titulo);
            return result;
        }

        public async Task<Livro> AtualizarLivroAsync(Livro livro)
        {
            var existente = await _repository.ListarAsync();
            if (existente.Any(l => l.Titulo.Equals(livro.Titulo, StringComparison.OrdinalIgnoreCase)) && existente.Any(l => l.Autor.Equals(livro.Autor, StringComparison.OrdinalIgnoreCase)))
            {
                Log.Warning("Livro {Titulo} do autor {Autor} já cadastrado", livro.Titulo, livro.Autor);
                throw new LivroJaCadastradoExcepton(livro.Titulo);
            }

            var result = await _repository.AtualizarAsync(livro);
            Log.Information("Livro {Titulo} atualizado com sucesso", livro.Titulo);
            return result;
        }

        public async Task<bool> DeletarLivroAsync(Guid id)
            => await _repository.RemoverAsync(id);

        public async Task<Livro> ObterLivroPorIdAsync(Guid id)
            => await _repository.ObterPorIdAsync(id);

        public async Task<IEnumerable<Livro>> ListarLivrosAsync()
            => (await _repository.ListarAsync()).OrderBy(l => l.Titulo);
    }

}
