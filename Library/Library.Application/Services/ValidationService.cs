using Library.Application.Exceptions;
using Library.Application.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace Library.Application.Services
{
    public class ValidationService : IValidationService
    {
        public async Task ValidateAsync<T>(IValidator<T> validator, T request)
        {
            ValidationResult result = await validator.ValidateAsync(request);

            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new BadRequestException(errors);
            }
        }
    }
}
