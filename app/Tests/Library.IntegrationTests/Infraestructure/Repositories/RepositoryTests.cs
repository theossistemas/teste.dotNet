using Audit.Core;
using Audit.Core.Providers;
using FluentAssertions;
using Library.Domain.Entities;
using Library.Infraestructure.AppDbContext;
using Library.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.IntegrationTests.Infraestructure.Repositories;

public class RepositoryTests
{
    private Repository<Login> CreateRepository(out DataContext context)
    {
        Configuration.Setup().UseNullProvider();
        Configuration.DataProvider = new NullDataProvider();

        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        context = new DataContext(options);
        return new Repository<Login>(context);
    }

    [Fact]
    public async Task Insert_ShouldAddEntity()
    {
        var repo = CreateRepository(out var context);
        var entity = Login.Create("Teste user", "teste@mail", "teste_password");

        var result = await repo.Insert(entity, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        context.Set<Login>().Count().Should().Be(1);
    }

    [Fact]
    public async Task GetById_ShouldReturnEntity()
    {
        var repo = CreateRepository(out var context);
        var entity = Login.Create("Teste user", "teste@mail", "teste_password");
        await repo.Insert(entity, CancellationToken.None);

        var result = await repo.GetById(entity.Id, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(entity.Id);
    }

    [Fact]
    public async Task GetAll_ShouldReturnPagedList()
    {
        var repo = CreateRepository(out var context);
        for (int i = 0; i < 15; i++)
        {
            var entity = Login.Create("Teste user", "teste@mail", "teste_password");
            await repo.Insert(entity, CancellationToken.None);
        }

        var result = await repo.GetAll(page: 2, pageSize: 5, cancellation: CancellationToken.None);

        result.Count.Should().Be(5);
        result.First().UserName.Should().Be("Teste user");
    }

    [Fact]
    public async Task Count_ShouldReturnCorrectCount()
    {
        var repo = CreateRepository(out var context);
        await repo.Insert(Login.Create("Teste user 1", "teste@mail", "teste_password"), CancellationToken.None);
        await repo.Insert(Login.Create("Teste user 2", "teste@mail", "teste_password"), CancellationToken.None);

        var total = await repo.Count(cancellation: CancellationToken.None);
        total.Should().Be(2);

        var filtered = await repo.Count(x => x.UserName == "Teste user 1", CancellationToken.None);
        filtered.Should().Be(1);
    }

    [Fact]
    public async Task FindOne_ShouldReturnCorrectEntity()
    {
        var repo = CreateRepository(out var context);
        var entity = Login.Create("Teste user", "teste@mail", "teste_password");
        await repo.Insert(entity, CancellationToken.None);

        var result = await repo.FindOne(x => x.UserName == "Teste user", CancellationToken.None);
        result.Should().NotBeNull();
        result.Id.Should().Be(entity.Id);
    }

    [Fact]
    public async Task Update_ShouldModifyEntity()
    {
        var repo = CreateRepository(out var context);
        var entity = Login.Create("Old name", "teste@mail", "teste_password");

        entity.SetUserName("New name");
         var result = await repo.Update(entity, CancellationToken.None);

        result.UserName.Should().Be("New name");
        context.Set<Login>().First().UserName.Should().Be("New name");
    }

    [Fact]
    public async Task Delete_ShouldRemoveEntity()
    {
        var repo = CreateRepository(out var context);
        var entity = Login.Create("User name", "teste@mail", "teste_password");
        await repo.Insert(entity, CancellationToken.None);

        context.ChangeTracker.Clear();

        await repo.Delete(entity.Id, CancellationToken.None);

        context.Set<Login>().Count().Should().Be(0);
    }

    [Fact]
    public async Task Delete_NonExistentEntity_ShouldThrowException()
    {
        var repo = CreateRepository(out var context);

        Func<Task> act = async () => await repo.Delete(Guid.NewGuid(), CancellationToken.None);
        await act.Should().ThrowAsync<ArgumentException>();
    }
}