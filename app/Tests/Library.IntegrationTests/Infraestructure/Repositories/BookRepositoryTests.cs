using Audit.Core;
using Audit.Core.Providers;
using FluentAssertions;
using Library.Domain.Entities;
using Library.Infraestructure.AppDbContext;
using Library.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.IntegrationTests.Infraestructure.Repositories;

public class BookRepositoryTests
{
    private BookRepository CreateRepository(out DataContext context)
    {
        Configuration.Setup().UseNullProvider();
        Configuration.DataProvider = new NullDataProvider();

        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // banco isolado por teste
            .Options;

        context = new DataContext(options);
        return new BookRepository(context);
    }

    [Fact]
    public async Task GetBookOrderByName_ShouldReturnAllBooks()
    {
        var repo = CreateRepository(out var context);

        var book1 = Book.Create("C Book", "Author 1", "Summary 1");
        var book2 = Book.Create("A Book", "Author 2", "Summary 2");
        var book3 = Book.Create("B Book", "Author 3", "Summary 3");

        await repo.Insert(book1, CancellationToken.None);
        await repo.Insert(book2, CancellationToken.None);
        await repo.Insert(book3, CancellationToken.None);

        var result = await repo.GetBookOrderByName();

        result.Count.Should().Be(3);
    }

    [Fact]
    public async Task GetBookOrderByName_ShouldFilterByName()
    {
        var repo = CreateRepository(out var context);

        await repo.Insert(Book.Create("C Book", "Author 1", "Summary 1"), CancellationToken.None);
        await repo.Insert(Book.Create("A Book", "Author 2", "Summary 2"), CancellationToken.None);
        await repo.Insert(Book.Create("B Book", "Author 3", "Summary 3"), CancellationToken.None);

        var result = await repo.GetBookOrderByName(x => x.Name.Contains("A"));

        result.Count.Should().Be(1);
        result.First().Name.Should().Be("A Book");
    }

    [Fact]
    public async Task GetBookOrderByName_ShouldOrderByNameAsc()
    {
        var repo = CreateRepository(out var context);

        await repo.Insert(Book.Create("C Book", "Author 1", "Summary 1"), CancellationToken.None);
        await repo.Insert(Book.Create("A Book", "Author 2", "Summary 2"), CancellationToken.None);
        await repo.Insert(Book.Create("B Book", "Author 3", "Summary 3"), CancellationToken.None);

        var result = await repo.GetBookOrderByName(orderBy: "asc");

        result[0].Name.Should().Be("A Book");
        result[1].Name.Should().Be("B Book");
        result[2].Name.Should().Be("C Book");
    }

    [Fact]
    public async Task GetBookOrderByName_ShouldPaginate()
    {
        var repo = CreateRepository(out var context);

        for (int i = 1; i <= 9; i++)
        {
            await repo.Insert(Book.Create($"Book {i}", $"Author {i}", $"Summary {i}"), CancellationToken.None);
        }

        var result = await repo.GetBookOrderByName(page: 1, pageSize: 3, orderBy: "desc");

        result.Count.Should().Be(3);
        result[0].Name.Should().Be("Book 9");
        result[1].Name.Should().Be("Book 8");
        result[2].Name.Should().Be("Book 7");
    }
}