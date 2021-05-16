using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class AccountInsertDTOValidator : AbstractValidator<AccountInsertDTO>, IDomainValidator
    {
        public AccountInsertDTOValidator()
        {
            RuleFor(v => v.ConfirmPassword).Equal(e => e.Password).WithMessage("The passwords is difference.");
            RuleFor(v => v.Password).NotEmpty().WithMessage("The password is required.");
            RuleFor(v => v.ConfirmPassword).NotEmpty().WithMessage("The password is required.");
            RuleFor(v => v.Email).EmailAddress().WithMessage("Email invalid.");
            RuleFor(v => v.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(v => v.Name).NotEmpty().WithMessage("Name is required.");
        }
    }
}
