using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class AccountUpdateDTOValidator : AbstractValidator<AccountUpdateDTO>, IDomainValidator
    {
        public AccountUpdateDTOValidator()
        {
            RuleFor(v => v.Email).EmailAddress().WithMessage("Email invalid.");
            RuleFor(v => v.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(v => v.Name).NotEmpty().WithMessage("Name is required.");
        }
    }
}
