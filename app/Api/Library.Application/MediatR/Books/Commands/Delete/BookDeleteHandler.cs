using MediatR;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Shared.Extensions;

namespace Library.Application.MediatR.Books.Commands.Delete;

public class BookDeleteHandler(IBookRepository bookRepository) : IRequestHandler<BookDeleteCommand, Unit>
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task<Unit> Handle(BookDeleteCommand command, CancellationToken cancellationToken)
    {
        await Book.Delete(command.Id.GuidParse(), _bookRepository, cancellationToken);
        return Unit.Value;
    }
}