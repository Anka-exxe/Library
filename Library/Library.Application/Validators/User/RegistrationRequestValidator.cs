using Library.Application.DTO.UserDto.Request;
using FluentValidation;

namespace Library.Application.Validators.User
{
    public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
    {
        public RegistrationRequestValidator()
        {
            RuleFor(r => r.Username)
                .NotEmpty().WithMessage("Login is required")
                .Length(4, 20).WithMessage("Login length must be from 4 to 20 characters")
                .Matches(@"^[A-Za-z0-9_]*$").WithMessage("Login must contain only letters, numbers or underline");

            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Password is required")
                .Length(6, 20).WithMessage("Password length must be from 8 to 20 characters");

            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Firstname is required")
                .Length(3, 50).WithMessage("Firstname length must be from 3 to 50 characters")
                .Matches(@"^[a-zA-Zа-яА-Я\s]+$").WithMessage("Firstname must contain only letters and spaces");

            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Lastname is required")
                .Length(3, 50).WithMessage("Lastname length must be from 3 to 50 characters")
                .Matches(@"^[a-zA-Zа-яА-Я\s]+$").WithMessage("Lastname must contain only letters and spaces");

            RuleFor(r => r.BirthDate)
                .LessThan(DateTime.Now).WithMessage("Birth date must be in the past.");

            RuleFor(r => r.Email)
               .NotEmpty().WithMessage("Email is required.")
               .EmailAddress().WithMessage("Incorrect email address");
        }
    }
}
