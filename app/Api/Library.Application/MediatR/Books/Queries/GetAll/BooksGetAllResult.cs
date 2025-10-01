using Library.Domain.Entities;

namespace Library.Application.MediatR.Books.Queries.GetAll;

public class BooksGetAllResult
{
    public Guid Id                  { get; set; } = default!;
    public string Name              { get; set; } = default!;
    public string Author            { get; set; } = default!;
    public string Summary           { get; set; } = default!;
    public bool Active              { get; set; } = default!;
    public DateTime CreatedAt       { get; set; } = default!;

    public static implicit operator BooksGetAllResult(Book entity)
        => new ()
        {
            Id = entity.Id,
            Name = entity.Name,
            Author = entity.Author,
            Summary = entity.Summary,
            Active = entity.Active,
            CreatedAt = entity.CreatedAt
        };
}