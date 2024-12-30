using FluentValidation;

namespace Library.Application.Interfaces
{
    public interface IValidationService
    {
        Task ValidateAsync<T>(IValidator<T> validator, T request);
    }
}
