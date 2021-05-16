using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>, IDomainValidator
    {
        public LoginDTOValidator()
        {
            RuleFor(v => v.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("Email invalid.");
            RuleFor(v => v.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}
