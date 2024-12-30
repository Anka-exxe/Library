using FluentValidation;
using Library.Application.DTO.UserDto.Request;

namespace Library.Application.Validators.User
{
    public class AddAdminRequestValidator : AbstractValidator<AddAdminRequest>
    {
        public AddAdminRequestValidator()
        {
            RuleFor(r => r.Username)
                .NotEmpty().WithMessage("Login is required")
                .Length(4, 20).WithMessage("Login length must be from 4 to 20 characters")
                .Matches(@"^[A-Za-z0-9_]*$").WithMessage("Login must contain only letters, numbers or underline");

            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Password is required")
                .Length(6, 20).WithMessage("Password length must be from 8 to 20 characters");

            RuleFor(r => r.Email)
               .NotEmpty().WithMessage("Email is required.")
               .EmailAddress().WithMessage("Incorrect email address");
        }
    }
}