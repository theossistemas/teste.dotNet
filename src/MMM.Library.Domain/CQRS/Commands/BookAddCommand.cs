using FluentValidation;
using MMM.IStore.Core.Messages;
using System;

namespace MMM.Library.Domain.CQRS.Commands
{
    public class BookAddCommand : Command
    {
        public Guid CategoryId { get; private set; }
        public string Title { get; private set; }
        public int Year { get; private set; }
        public string Language { get; private set; }
        public string Location { get; private set; }

        public BookAddCommand(Guid categoryId, string title, int year, string language, string location)
        {
            CategoryId = categoryId;
            Title = title;
            Year = year;
            Language = language;
            Location = location;
        }

        public override bool IsValid()
        {
            ValidationResult = new BookAddCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class BookAddCommandValidation : AbstractValidator<BookAddCommand>
    {
        public BookAddCommandValidation()
        {
            RuleFor(c => c.CategoryId)
                .NotEqual(Guid.Empty)
                .WithMessage("Categoria Inválida");

            RuleFor(c => c.Title)
                .NotEmpty();

            RuleFor(c => c.Year)
                .InclusiveBetween(1800, DateTime.Now.Year)
                .WithMessage("Ano do Livro: Permitido valores entre 1800 e ano atual"); ;

            RuleFor(c => c.Language)
                .NotEmpty()
                .WithMessage("Linguagem do Livro não informada");
        }
    }

}
