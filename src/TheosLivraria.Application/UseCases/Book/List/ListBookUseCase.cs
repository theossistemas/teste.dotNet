using FluentValidation;
using TheosLivraria.Application.UseCases.Abstractions;
using TheosLivraria.Application.UseCases.Book.list.DTOs;
using TheosLivraria.Application.UseCases.Commom;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Models.Entities;
using TheosLivraria.Domain.Models.Filters.Book;

namespace TheosLivraria.Application.UseCases.Book.list
{
    public class ListBooksUseCase : IUseCase<ListBooksInput, ListBooksOutput>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IValidator<ListBooksInput> _validator;

        public ListBooksUseCase(IBookRepository bookRepository, IValidator<ListBooksInput> validator)
        {
            _bookRepository = bookRepository;
            _validator = validator;
        }

        public async Task<Result<ListBooksOutput>> ExecuteAsync(ListBooksInput input)
        {
            var validation = await _validator.ValidateAsync(input);
            if (!validation.IsValid)
            {
                var errors = validation.Errors
                    .Select(e => new { Property = e.PropertyName, Error = e.ErrorMessage })
                    .ToList();

                return Result<ListBooksOutput>.Fail("Erro de validação na entidade.", errors);
            }

            var filter = new ListBookFilter
            {
                Page = input.Page,
                Size = input.PageSize
            };

            (List<BookEntity> books, int totalCount) =
                await _bookRepository.ListBooksAsync(filter);

            var output = new ListBooksOutput
            {
                TotalCount = totalCount,
                Data = books.Select(b => new ListBooksOutput.BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Description = b.Description,
                    Price = b.Price,
                    StockQuantity = b.StockQuantity,
                    PublishedDate = b.PublishedDate
                }).ToList()
            };

            return Result<ListBooksOutput>.Ok(output);
        }
    }
}
