using FluentValidation;
using TheosLivraria.Application.UseCases.Abstractions;
using TheosLivraria.Application.UseCases.Book.GetById.DTOs;
using TheosLivraria.Application.UseCases.Commom;
using TheosLivraria.Domain.Interfaces.Repository;

namespace TheosLivraria.Application.UseCases.Book.GetById
{
    public class GetBookByIdUseCase : IUseCase<GetBookByIdInput, GetBookByIdOutput>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IValidator<GetBookByIdInput> _validator;

        public GetBookByIdUseCase(IBookRepository bookRepository, IValidator<GetBookByIdInput> validator)
        {
            _bookRepository = bookRepository;
            _validator = validator;
        }

        public async Task<Result<GetBookByIdOutput>> ExecuteAsync(GetBookByIdInput input)
        {
            var validation = await _validator.ValidateAsync(input);
            if (!validation.IsValid)
            {
                var errors = validation.Errors
                    .Select(e => new { Property = e.PropertyName, Error = e.ErrorMessage })
                    .ToList();

                return Result<GetBookByIdOutput>.Fail("Erro de validação na entidade.", errors);
            }

            var book = await _bookRepository.GetByIdAsync(input.Id);
            if (book == null)
            {
                return Result<GetBookByIdOutput>.Fail("Livro não encontrado.");
            }

            var output = new GetBookByIdOutput
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                Price = book.Price,
                StockQuantity = book.StockQuantity,
                PublishedDate = book.PublishedDate
            };

            return Result<GetBookByIdOutput>.Ok(output);
        }
    }
}
