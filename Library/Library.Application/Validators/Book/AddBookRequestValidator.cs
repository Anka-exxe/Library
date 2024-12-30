using FluentValidation;
using Library.Application.DTO.BookDto;

namespace Library.Application.Validators.Book
{
    public class AddBookRequestValidator : AbstractValidator<AddBookRequest>
    {
        public AddBookRequestValidator()
        {
            RuleFor(b => b.Book)
                .NotNull().WithMessage("Book is required")
                .SetValidator(new BookBaseDtoValidator());
        }
    }
}
