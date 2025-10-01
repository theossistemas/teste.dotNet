using MediatR;
using Library.Application.Common.Paged;

namespace Library.Application.MediatR.Books.Queries.GetAll;

public class BooksGetAllQuery : GetPaged, IRequest<PagedResult<BooksGetAllResult>>
{
    public string? Name { get; set; } = null;
}