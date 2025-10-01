using MediatR;
using Library.Domain.Entities;

namespace Library.Application.MediatR.Books.Commands.Update;

public record BookUpdateCommand : IRequest<Unit>
{
    public string Id        { get; set; } = default!;
    public string Name      { get; set; } = default!;
    public string Author    { get; set; } = default!;
    public string Summary   { get; set; } = default!;

    public static implicit operator Book(BookUpdateCommand dto)
        => Book.Create(dto.Name, dto.Author, dto.Summary);
}