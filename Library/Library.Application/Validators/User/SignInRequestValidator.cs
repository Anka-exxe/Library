using Library.Application.DTO.UserDto.Request;
using FluentValidation;

namespace Library.Application.Validators.User
{
    public class SignInRequestValidator : AbstractValidator<SignInRequest>
    {
        public SignInRequestValidator()
        {
            RuleFor(r => r.Login)
                .NotEmpty().WithMessage("Login is required")
                .Length(4, 20).WithMessage("The login length must be from 4 to 20 characters")
                .Matches(@"^[A-Za-z0-9_]*$").WithMessage("Login must contain only letters, numbers or underline");

            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Password is required")
                .Length(6, 20).WithMessage("The password length must be from 6 to 20 characters");
        }
    }
}
