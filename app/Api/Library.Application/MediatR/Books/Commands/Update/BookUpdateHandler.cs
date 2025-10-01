using MediatR;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Shared.Extensions;

namespace Library.Application.MediatR.Books.Commands.Update;

public class BookUpdateHandler(IBookRepository bookRepository) : IRequestHandler<BookUpdateCommand, Unit>
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task<Unit> Handle(BookUpdateCommand command, CancellationToken cancellationToken)
    {
        await Book.ExistsBookByNameUpdate(_bookRepository, command.Id.GuidParse(), command.Name, cancellationToken);
        await Book.Update(command.Id.GuidParse(), command.Name, command.Author, command.Summary, _bookRepository, cancellationToken);
        return Unit.Value;
    }
}