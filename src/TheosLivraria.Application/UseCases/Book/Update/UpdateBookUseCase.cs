using FluentValidation;
using TheosLivraria.Application.UseCases.Abstractions;
using TheosLivraria.Application.UseCases.Book.Create.DTOs;
using TheosLivraria.Application.UseCases.Book.Update.DTOs;
using TheosLivraria.Application.UseCases.Commom;
using TheosLivraria.Domain.Interfaces.Data;
using TheosLivraria.Domain.Interfaces.Repository;

namespace TheosLivraria.Application.UseCases.Book.Update
{
    public class UpdateBookUseCase : IUseCase<UpdateBookInput, UpdateBookOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;
        private readonly IValidator<UpdateBookInput> _validator;

        public UpdateBookUseCase(
            IBookRepository bookRepository,
            IUnitOfWork unitOfWork,
            IValidator<UpdateBookInput> validator)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<UpdateBookOutput>> ExecuteAsync(UpdateBookInput input)
        {
            var validation = await _validator.ValidateAsync(input);

            if (!validation.IsValid)
            {
                var errors = validation.Errors
                    .Select(e => new { Property = e.PropertyName, Error = e.ErrorMessage })
                    .ToList();

                return Result<UpdateBookOutput>.Fail("Erro de validação na entidade.", errors);
            }

            var book = await _bookRepository.GetByIdAsync(input.Id);
            if (book == null)
            {
                return Result<UpdateBookOutput>.Fail("Livro não encontrado.");
            }

            var existingBookWithTitle = await _bookRepository.GetByTitleAsync(input.Title);
            if (existingBookWithTitle != null && existingBookWithTitle.Id != book.Id)
            {
                return Result<UpdateBookOutput>.Fail("O título do livro informado já está em uso.");
            }

            book.Title = input.Title;
            book.Author = input.Author;
            book.Description = input.Description;
            book.StockQuantity = input.StockQuantity;
            book.Price = input.Price;
            book.PublishedDate = input.PublishedDate;

            _bookRepository.Update(book);
            await _unitOfWork.CommitAsync();

            return Result<UpdateBookOutput>.Ok(new UpdateBookOutput { Id = book.Id });
        }
    }
}
