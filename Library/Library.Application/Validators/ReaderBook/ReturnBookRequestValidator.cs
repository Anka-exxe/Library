using FluentValidation;
using Library.Application.DTO.ReaderBookDto;

namespace Library.Application.Validators.ReaderBook
{
    public class ReturnBookRequestValidator : AbstractValidator<ReturnBookRequest>
    {
        public ReturnBookRequestValidator()
        {
            RuleFor(r => r.BookISBN)
                .NotEmpty().WithMessage("Book is required");
        }
    }
}
