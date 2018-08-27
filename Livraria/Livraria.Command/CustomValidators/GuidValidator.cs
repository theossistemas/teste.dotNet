using FluentValidation.Validators;
using System;

namespace Livraria.Command.CustomValidators
{
    public class GuidValidator : PropertyValidator
    {
        public GuidValidator() : base("{PropertyName} não é um Guid válido.")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            Guid valor;
            return Guid.TryParse(context?.PropertyValue?.ToString(), out valor);
        }
    }
}
