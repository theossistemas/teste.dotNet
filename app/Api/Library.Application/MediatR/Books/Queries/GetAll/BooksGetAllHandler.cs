using MediatR;
using Library.Shared.Validators;
using Library.Domain.Interfaces;
using Library.Application.Common.Paged;

namespace Library.Application.MediatR.Books.Queries.GetAll;

public class BooksGetAllHandler(IBookRepository bookRepository) : IRequestHandler<BooksGetAllQuery, PagedResult<BooksGetAllResult>>
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task<PagedResult<BooksGetAllResult>> Handle(BooksGetAllQuery query, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetBookOrderByName(
            filter: (x => !query.Name.IsEmpty() 
                ? x.Name.Contains(query.Name!) && x.Active 
                : x.Active),
            page: query.Page!.Value,
            pageSize: query.PageSaze!.Value,
            orderBy: query.OrderBy,
            cancellation: cancellationToken);

        var booksTotalCount = await _bookRepository.Count(cancellation: cancellationToken);

        return books.MapToResult(booksTotalCount, query.Page, query.PageSaze);
    }
}