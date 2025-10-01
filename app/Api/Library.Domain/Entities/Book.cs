using Library.Domain.Messages;
using Library.Domain.Interfaces;
using Library.Domain.Entities.Base;

namespace Library.Domain.Entities;

public class Book : BaseEntity
{
    public Book(
        Guid id,
        string name,
        string author,
        string summary,
        bool active,
        DateTime createdAt)
    {
        Id = id;
        Name = name;
        Author = author;
        Summary = summary;
        Active = active;
        CreatedAt = createdAt;
    }

    public Book(
        string name,
        string author,
        string summary)
    {
        Name = name;
        Author = author;
        Summary = summary;
    }

    internal Book() { }

    public string Name      { get; private set; } = default!;
    public string Author    { get; private set; } = default!;
    public string Summary   { get; private set; } = default!;

    public static Book Create(
        string name,
        string author,
        string summary)
    {
        return new Book(
            name,
            author,
            summary);
    }

    public static async Task<Book> Update(
        Guid id,
        string name,
        string author,
        string summary,
        IBookRepository repository,
        CancellationToken cancellation)
    {
        var bookEntity = await repository.GetById(id, cancellation);

        if (bookEntity is null)
            throw new ArgumentException(string.Format(EntityMessages.EMPTY, "Livro", $"id {id}"));

        var bookChanged = new Book(
            id,
            name,
            author,
            summary,
            bookEntity.Active,
            bookEntity.CreatedAt);

        return await repository.Update(bookChanged, cancellation);
    }

    public static async Task Delete(
        Guid id,
        IBookRepository repository,
        CancellationToken cancellation)
    {
        await repository.Delete(id, cancellation);
    }

    public static async Task ExistsBookByName(IBookRepository repo, string name, CancellationToken cancellation)
    {
        bool validate = await repo.Count(x => x.Name.Equals(name), cancellation) > 0;

        if (validate)
            throw new ArgumentException(string.Format(EntityMessages.HAS_VALUE, "Livro", $"nome ({name})"));
    }

    public static async Task ExistsBookByNameUpdate(IBookRepository repo, Guid id, string name, CancellationToken cancellation)
    {
        bool validate = await repo.Count(x => x.Name.Equals(name) && x.Id != id, cancellation) > 0;

        if (validate)
            throw new ArgumentException(string.Format(EntityMessages.HAS_VALUE, "Livro", $"nome ({name})"));
    }
}