using Library.Application.DTO.AuthorDto;
using FluentValidation;

namespace Library.Application.Validators.Author
{
    public class UpdateAuthorRequestValidator : AbstractValidator<UpdateAuthorRequest>
    {
        public UpdateAuthorRequestValidator()
        {
            RuleFor(a => a.AuthorId)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(a => a.Author)
                .NotNull().WithMessage("Author is required")
                .SetValidator(new AuthorBaseDTOValidator());
        }
    }
}
