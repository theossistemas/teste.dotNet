using System;

using FluentValidation.Results;

namespace TheosBookStore.LibCommon.Services
{
    public abstract class ServiceBase
    {
        private ValidationResult _validationResult;
        protected ValidationResult ValidationResult
        {
            get => _validationResult ?? (_validationResult = new ValidationResult());
        }

        public string GetErrorMessages()
        {
            return string.Join(". ", ValidationResult.Errors);
        }

        public bool IsValid
        {
            get
            {
                return ValidationResult.IsValid;
            }
        }

        protected void AddErrorMessage(string errorMessage)
        {
            var validationFailure = new ValidationFailure(
                propertyName: string.Empty,
                errorMessage);
            ValidationResult.Errors.Add(validationFailure);
        }

        protected void AddErrorMessage(ValidationFailure validationFailure)
        {
            ValidationResult.Errors.Add(validationFailure);
        }

        protected void ExceptionHandler(Exception exception)
        {
            var hasMessage = !string.IsNullOrEmpty(exception.Message);
            if (hasMessage)
                AddErrorMessage(exception.Message);

            var hasInnerException = exception.InnerException != null;
            if (hasInnerException)
                ExceptionHandler(exception.InnerException);

            var isAggregateException = (exception is AggregateException);
            if (!isAggregateException)
                return;
            AggregateExceptionHandler((AggregateException)exception);
        }

        private void AggregateExceptionHandler(AggregateException exception)
        {
            AddErrorMessage("With following Inner exceptions:");
            foreach (var innerException in exception.InnerExceptions)
                ExceptionHandler(innerException);
        }
    }
}
