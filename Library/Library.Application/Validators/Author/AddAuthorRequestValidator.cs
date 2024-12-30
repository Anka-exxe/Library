using Library.Application.DTO.AuthorDto;
using FluentValidation;

namespace Library.Application.Validators.Author
{
    public class AddAuthorRequestValidator : AbstractValidator<AddAuthorRequest>
    {
        public AddAuthorRequestValidator()
        {
            RuleFor(a => a.Author)
                .NotNull().WithMessage("Author is required")
                .SetValidator(new AuthorBaseDTOValidator());
        }
    }
}
