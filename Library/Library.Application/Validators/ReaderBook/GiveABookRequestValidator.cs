using FluentValidation;
using Library.Application.DTO.ReaderBookDto;

namespace Library.Application.Validators.ReaderBook
{
    public class GiveABookRequestValidator : AbstractValidator<GiveABookRequest>
    {
        public GiveABookRequestValidator()
        {
            RuleFor(r => r.ReaderId)
                .NotEmpty().WithMessage("Reader is required");

            RuleFor(r => r.BookISBN)
                .NotEmpty().WithMessage("Book is required");

            RuleFor(r => r.ReceiptDate)
                .NotEmpty().WithMessage("Taking date is required");

            RuleFor(r => r.ReturnDate)
                .NotEmpty().WithMessage("Return date is required")
                .GreaterThan(r => r.ReceiptDate).WithMessage("Return date must be greater than taking date.");
        }
    }
}
