using MediatR;
using Library.Domain.Entities;

namespace Library.Application.MediatR.Books.Commands.Create;

public record BookCreateCommand : IRequest<BookCreateResult>
{
    public string Name      { get; set; } = default!;
    public string Author    { get; set; } = default!;
    public string Summary   { get; set; } = default!;

    public static implicit operator Book(BookCreateCommand dto)
        => Book.Create(dto.Name, dto.Author, dto.Summary);
}