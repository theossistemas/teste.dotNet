using FluentValidation;
using TheosLivraria.Application.UseCases.Abstractions;
using TheosLivraria.Application.UseCases.Book.Create.DTOs;
using TheosLivraria.Application.UseCases.Commom;
using TheosLivraria.Domain.Interfaces.Data;
using TheosLivraria.Domain.Interfaces.Repository;
using TheosLivraria.Domain.Models.Entities;

namespace TheosLivraria.Application.UseCases.Book.Create
{
    public class CreateBookUseCase : IUseCase<CreateBookInput, CreateBookOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;
        private readonly IValidator<CreateBookInput> _validator;

        public CreateBookUseCase(
            IBookRepository bookRepository,
            IUnitOfWork unitOfWork,
            IValidator<CreateBookInput> validator)
        {
            _bookRepository = bookRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<CreateBookOutput>> ExecuteAsync(CreateBookInput input)
        {
            var validation = await _validator.ValidateAsync(input);

            if (!validation.IsValid)
            {
                var errors = validation.Errors
                    .Select(e => new { Property = e.PropertyName, Error = e.ErrorMessage })
                    .ToList();

                return Result<CreateBookOutput>.Fail("Erro de validação na entidade.", errors);
            }

            var existingBook = await _bookRepository.GetByTitleAsync(input.Title);
            if (existingBook != null)
            {
                return Result<CreateBookOutput>.Fail("O título do livro informado já está em uso.");
            }

            var newBook = new BookEntity
            {
                Title = input.Title,
                Author = input.Author,
                Description = input.Description,
                StockQuantity = input.StockQuantity,
                Price = input.Price,
                PublishedDate = input.PublishedDate
            };

            await _bookRepository.AddAsync(newBook);
            await _unitOfWork.CommitAsync();

            return Result<CreateBookOutput>.Ok(new CreateBookOutput { Id = newBook.Id });
        }

    }
}
