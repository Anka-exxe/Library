using FluentValidation;
using Library.Application.DTO.AuthorDto;

namespace Library.Application.Validators.Author
{
    public class AuthorBaseDTOValidator : AbstractValidator<AuthorBaseDTO>
    {
        public AuthorBaseDTOValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Author's name can not be empty")
                .Length(3, 20).WithMessage("Name length must be from 3 to 20 characters ");

            RuleFor(a => a.Surname)
                .NotEmpty().WithMessage("Surname can not be empty")
                .Length(3, 30).WithMessage("Surname length must be from 3 to 30 characters ");

            RuleFor(a => a.BirthDate)
                .LessThan(DateTime.Now).WithMessage("Birth date must be in the past.");

            RuleFor(a => a.Country)
                .Length(3, 20).WithMessage("Country length must be between 3 and 20 characters")
                .Matches(@"^[A-Za-zА-Яа-я\s]*$").WithMessage("Author's country must contain only letters and spaces");
        }
    }
}
