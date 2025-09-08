using Livraria.Infrastructure.Repositories;
using Livraria.Domain;
using Livraria.Infrastructure;
using Livraria.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Livraria.Tests.Read
{
    public class LivroReadRepositoryTests
    {
        private LivrariaDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<LivrariaDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new LivrariaDbContext(options);
        }

        private Livro GetLivro(int id = 1, bool ativo = true)
        {
            return new Livro(id, "Título", "Autor", "1234567890", 2020, 10, 99.99m)
            {
                Ativo = ativo
            };
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarLivro_QuandoEncontrado()
        {
            var db = GetDbContext();
            var logger = new Mock<ILogger<LivroReadRepository>>();
            var repo = new LivroReadRepository(db, logger.Object);

            var livro = GetLivro();
            db.Livros.Add(livro);
            await db.SaveChangesAsync();

            var result = await repo.GetByIdAsync(livro.Id);

            Assert.NotNull(result);
            Assert.Equal(livro.Id, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_DeveLancarNaoEncontradoException_QuandoNaoEncontrado()
        {
            var db = GetDbContext();
            var logger = new Mock<ILogger<LivroReadRepository>>();
            var repo = new LivroReadRepository(db, logger.Object);

            await Assert.ThrowsAsync<NaoEncontradoException>(() => repo.GetByIdAsync(999));
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarLivros_QuandoExistemLivrosAtivos()
        {
            var db = GetDbContext();
            var logger = new Mock<ILogger<LivroReadRepository>>();
            var repo = new LivroReadRepository(db, logger.Object);

            db.Livros.Add(GetLivro(1, true));
            db.Livros.Add(GetLivro(2, false));
            db.Livros.Add(GetLivro(3, true));
            await db.SaveChangesAsync();

            var result = await repo.GetAllAsync();

            Assert.NotNull(result);
            Assert.All(result, l => Assert.True(l.Ativo));
        }

        [Fact]
        public async Task GetAllAsync_DeveLancarNaoEncontradoException_QuandoNaoExistemLivrosAtivos()
        {
            var db = GetDbContext();
            var logger = new Mock<ILogger<LivroReadRepository>>();
            var repo = new LivroReadRepository(db, logger.Object);

            db.Livros.Add(GetLivro(1, false));
            await db.SaveChangesAsync();

            await Assert.ThrowsAsync<NaoEncontradoException>(() => repo.GetAllAsync());
        }

        [Fact]
        public async Task GetPagedAsync_DeveRetornarLivrosEPaginacao_QuandoExistemLivrosAtivos()
        {
            var db = GetDbContext();
            var logger = new Mock<ILogger<LivroReadRepository>>();
            var repo = new LivroReadRepository(db, logger.Object);

            db.Livros.Add(GetLivro(1, true));
            db.Livros.Add(GetLivro(2, true));
            db.Livros.Add(GetLivro(3, true));
            await db.SaveChangesAsync();

            var (livros, totalCount) = await repo.GetPagedAsync(1, 2);

            Assert.NotNull(livros);
            Assert.Equal(3, totalCount);
            Assert.True(livros is IEnumerable<Livro>);
        }

        [Fact]
        public async Task GetPagedAsync_DeveLancarNaoEncontradoException_QuandoNaoExistemLivrosAtivos()
        {
            var db = GetDbContext();
            var logger = new Mock<ILogger<LivroReadRepository>>();
            var repo = new LivroReadRepository(db, logger.Object);

            await Assert.ThrowsAsync<NaoEncontradoException>(() => repo.GetPagedAsync(1, 2));
        }
    }
}
