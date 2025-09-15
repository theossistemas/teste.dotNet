namespace Livraria.Infrastructure.Repositories
{
    using Dapper;
    using Livraria.Domain.Entities;
    using Livraria.Domain.Interfaces;
    using System.Data;

    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly IDbConnection _dbConnection;

        public CategoriaRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Categoria> AdicionarAsync(Categoria categoria)
        {
            var sql = @"INSERT INTO Categoria (Id, Nome) VALUES (@Id, @Nome)";
            await _dbConnection.ExecuteAsync(sql, categoria);
            return categoria;
        }

        public async Task<Categoria> AtualizarAsync(Categoria categoria)
        {
            var sql = @"UPDATE Categoria SET Nome=@Nome WHERE Id=@Id";
            await _dbConnection.ExecuteAsync(sql, categoria);
            return categoria;
        }

        public async Task<bool> RemoverAsync(Guid id)
        {
            var sql = "DELETE FROM Categoria WHERE Id=@Id";
            var rows = await _dbConnection.ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }

        public async Task<Categoria> ObterPorIdAsync(Guid id)
        {
            var sql = "SELECT * FROM Categoria WHERE Id=@Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Categoria>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Categoria>> ListarAsync()
        {
            var sql = "SELECT * FROM Categoria";
            return await _dbConnection.QueryAsync<Categoria>(sql);
        }
    }

}
