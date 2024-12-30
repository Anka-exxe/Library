using AutoMapper;
using FluentValidation;
using Library.Application.DTO.ReaderBookDto;
using Library.Application.Exceptions;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;

namespace Library.Application.Use_Cases.ReaderBookUseCase
{
    public class ReturnBook
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IValidator<ReturnBookRequest> validator;
        private readonly IValidationService validationService;

        public ReturnBook(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<ReturnBookRequest> validator, IValidationService validationService)
        {
            this.unitOfWork = unitOfWork;
            this.validator = validator;
            this.validationService = validationService;
        }

        public async Task ExecuteAsync(ReturnBookRequest request)
        {
            await validationService.ValidateAsync(validator, request);

            ReaderBook takenBook = await unitOfWork.ReaderBookRepository.GetByISBNAsync(request.BookISBN);

            if (await unitOfWork.BookRepository.GetByISBNAsync(request.BookISBN) == null)
            {
                throw new NotFoundException("Book not found");
            }
            if (takenBook == null)
            {
                throw new NotFoundException("Book is in Library");
            }

            await unitOfWork.ReaderBookRepository.DeleteAsync(takenBook);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
