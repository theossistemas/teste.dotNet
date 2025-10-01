using Library.Domain.Entities;
using Library.Application.Common.Paged;

namespace Library.Application.MediatR.Books.Queries.GetAll;

public static class BooksGetAllMapper
{
    public static PagedResult<BooksGetAllResult> MapToResult(this List<Book> books, int totalCount, int? page, int? pageSize)
    {
        var listOfBooksResult = books.Select(e => (BooksGetAllResult)e).ToList();

        return new PagedResult<BooksGetAllResult>(
            items: listOfBooksResult,
            totalCount: totalCount,
            page: page!.Value,
            pageSize: pageSize!.Value);
    }
}