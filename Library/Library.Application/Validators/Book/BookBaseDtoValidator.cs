using FluentValidation;
using Library.Application.DTO.BookDto;

namespace Library.Application.Validators.Book
{
    public class BookBaseDtoValidator : AbstractValidator<BookBaseDto>
    {
        public BookBaseDtoValidator()
        {
            RuleFor(r => r.ISBN)
                .NotEmpty().WithMessage("ISBN is required")
                .Matches(@"^(978|979)-\d{1,5}-\d{1,7}-\d{1,7}-\d{1}$")
                    .WithMessage("ISBN must be a valid ISBN-13 format (e.g., 978-3-16-148410-0)")
                .Matches(@"^\d{9}[\dX]$").When(r => r.ISBN.Length == 10)
                    .WithMessage("ISBN must be a valid ISBN-10 format (e.g., 0-306-40615-2)");

            RuleFor(r => r.Title)
                .NotEmpty().WithMessage("Book's name is required")
                .Length(1, 30).WithMessage("Book's name must be between 1 and 30 characters long.")
                .Matches(@"^[A-Za-zА-Яа-я0-9\s]*$").WithMessage("Book's name must contain only letters, numbers or spaces");

            RuleFor(r => r.Genre)
                .NotEmpty().WithMessage("Book's genre is required")
                .Length(1, 20).WithMessage("Book's genre must be between 1 and 20 characters long.")
                .Matches(@"^[A-Za-zА-Яа-я\s]*$").WithMessage("Book's genre must contain only letters or spaces");

            RuleFor(r => r.Description)
                .NotEmpty().WithMessage("Book's description is required")
                .Length(1, 255).WithMessage("Book's description must be between 1 and 255 characters long.");

            RuleFor(r => r.AuthorId)
                .NotEmpty().WithMessage("Author is required");
        }
    }
}
