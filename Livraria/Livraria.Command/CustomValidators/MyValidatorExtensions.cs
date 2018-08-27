using FluentValidation;

namespace Livraria.Command.CustomValidators
{
    public static class MyValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> IsGuid<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new GuidValidator());
        }
    }
}
