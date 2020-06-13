using FluentValidation;
using MMM.IStore.Core.Messages;
using System;

namespace MMM.Library.Domain.CQRS.Commands
{
    public class BookUpdateCommand : Command
    {
        public Guid Id { get; protected set; }
        public Guid CategoryId { get; private set; }
        public string Title { get; private set; }
        public int Year { get; private set; }
        public string Language { get; private set; }
        public string Location { get; private set; }

        public BookUpdateCommand(Guid id, Guid categoryId, string title, int year, string language, string location)
        {
            Id = id;
            CategoryId = categoryId;
            Title = title;
            Year = year;
            Language = language;
            Location = location;
        }

        public override bool IsValid()
        {
            ValidationResult = new BookUpdateCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class BookUpdateCommandValidation : AbstractValidator<BookUpdateCommand>
    {
        public BookUpdateCommandValidation()
        {
            RuleFor(c => c.CategoryId)
                .NotEqual(Guid.Empty)
                .WithMessage("Categoria Inválida");

            RuleFor(c => c.Title)
                .NotEmpty();

            RuleFor(c => c.Year)
                .InclusiveBetween(1800, DateTime.Now.Year)
                .WithMessage("Permitido valores entre 1800 e ano atual"); ;

            RuleFor(c => c.Language)
                .NotEmpty()
                .WithMessage("Linguagem não informada");
        }
    }

}
