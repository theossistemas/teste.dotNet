using LivrariaWeb.Data;
using LivrariaWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LivrariaWeb.Services.Livro
{
    public class LivroService : ILivroService
    {
        private readonly LivrariaDbContext _context;
        private readonly ILogger<LivroService> _logger;


        public LivroService(LivrariaDbContext context, ILogger<LivroService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<LivroModel>> ListarLivros()
        {
            _logger.LogInformation("Listando todos os livros de forma ascendente pelo nome.");
            return await _context.Livros.OrderBy(x => x.Titulo).ToListAsync();
        }

        public async Task<LivroModel> BuscarLivroPorId(int? idLivro)
        {
            if (idLivro == null || idLivro <= 0)
            {
                _logger.LogWarning("ID do livro inválido: {IdLivro}", idLivro);
                throw new ArgumentException("ID do livro inválido.");
            }

            var livro = await _context.Livros.FindAsync(idLivro);

            if (livro == null)
            {
                _logger.LogWarning("Livro não encontrado para o ID: {IdLivro}", idLivro);
                throw new KeyNotFoundException("Livro não encontrado.");
            }

            return livro;
        }

        public async Task<LivroModel> CriarLivro(LivroModel livro)
        {
            if (await LivroExistsAsync(livro.Titulo, livro.Autor))
            {
                _logger.LogWarning("Livro já cadastrado: {Titulo}, {Autor}", livro.Titulo, livro.Autor);
                throw new ArgumentException("Livro já cadastrado.");
            }

            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();
            return livro;
        }

        public async Task<LivroModel> EditarLivro(LivroModel livro)
        {
            var livroAtual = await _context.Livros.FindAsync(livro.Id);

            if (livroAtual is null)
            {
                _logger.LogWarning("Livro não encontrado para o ID: {IdLivro}", livro.Id);
                throw new ArgumentException("Livro não encontrado.");
            }

            // Atualizar propriedades do livro existente
            livroAtual.Titulo = livro.Titulo;
            livroAtual.Autor = livro.Autor;
            livroAtual.Preco = livro.Preco;

            _context.Entry(livroAtual).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Editando livro: {nomeLivro} - {idLivro}", livro.Titulo, livro.Id);
            return livroAtual;
        }

        public async Task<bool> ExcluirLivro(int? idLivro)
        {
            var livro = await _context.Livros.FindAsync(idLivro);

            if (livro == null)
            {
                _logger.LogWarning("ID do livro inválido: {IdLivro}", idLivro);
                return false;
            }

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Livro excluído: {nomeLivro} - {IdLivro}", livro.Titulo, idLivro);
            return true;
        }

        public async Task<bool> LivroExistsAsync(string? titulo, string? autor)
        {
            return await _context.Livros.AnyAsync(l => l.Titulo == titulo && l.Autor == autor);
        }
    }
}
