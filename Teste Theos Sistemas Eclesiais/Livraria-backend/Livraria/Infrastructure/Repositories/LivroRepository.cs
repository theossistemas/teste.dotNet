
using Dapper;
using Livraria.API.DTOs.Livros;
using Livraria.Domain.Interfaces;
using System.Data;

namespace Livraria.Infrastructure.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly IDbConnection _dbConnection;

        public LivroRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Livro> AdicionarAsync(Livro livro)
        {
            var sql = @"INSERT INTO Livro
            (Id, Titulo, Autor, AnoPublicacao, ISBN, NumeroPaginas, Editora, Idioma, Preco, Sinopse, DataCadastro, Disponivel,CategoriaId)
            VALUES (@Id, @Titulo, @Autor, @AnoPublicacao, @ISBN, @NumeroPaginas, @Editora, @Idioma, @Preco, @Sinopse, @DataCadastro, @Disponivel,@CategoriaId)";
            await _dbConnection.ExecuteAsync(sql, livro);
            return livro;
        }

        public async Task<Livro> AtualizarAsync(Livro livro)
        {
            var sql = @"UPDATE Livro SET 
            Titulo=@Titulo, Autor=@Autor, AnoPublicacao=@AnoPublicacao,Preco=@Preco WHERE Id=@Id";
            await _dbConnection.ExecuteAsync(sql, livro);
            return livro;
        }

        public async Task<bool> RemoverAsync(Guid id)
        {
            var sql = "DELETE FROM Livro WHERE Id=@Id";
            var rows = await _dbConnection.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }

        public async Task<Livro> ObterPorIdAsync(Guid id)
        {
            var sql = "SELECT * FROM Livro WHERE Id=@Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Livro>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Livro>> ListarAsync()
        {
            var sql = "SELECT l.Id,l.Titulo, l.Autor,l.AnoPublicacao,l.ISBN,l.NumeroPaginas,l.Editora,l.CategoriaId,c.Nome AS NomeCategoria,l.Idioma,l.Preco,l.Sinopse,l.DataCadastro,l.Disponivel FROM Livro l INNER JOIN Categoria c ON l.CategoriaId = c.Id ORDER BY l.Titulo";
            return await _dbConnection.QueryAsync<Livro>(sql);
        }
    }

}
