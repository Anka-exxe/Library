using AutoMapper;
using FluentValidation;
using Library.Application.DTO.ReaderBookDto;
using Library.Application.Exceptions;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;

namespace Library.Application.Use_Cases.ReaderBookUseCase
{
    public class GiveABook
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IValidator<GiveABookRequest> validator;
        private readonly IValidationService validationService;
        private readonly IEmailService emailService;

        public GiveABook(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<GiveABookRequest> validator, IValidationService validationService,
            IEmailService emailService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validator = validator;
            this.validationService = validationService;
            this.emailService = emailService;
        }

        public async Task ExecuteAsync(GiveABookRequest request)
        {
            await validationService.ValidateAsync(validator, request);

            Reader? reader = await unitOfWork.ReaderRepository.GetByUserIdAsync(request.ReaderId);

            if (reader == null)
            {
                throw new NotFoundException("Reader not found");
            }
            if (await unitOfWork.BookRepository.GetByISBNAsync(request.BookISBN) == null)
            {
                throw new NotFoundException("Book not found");
            }

            ReaderBook takenBook = await unitOfWork.ReaderBookRepository.GetByISBNAsync(request.BookISBN);

            if (takenBook != null) 
            {
                throw new BadRequestException("Book is already taken");
            }

            User user = await unitOfWork.UserRepository.GetByIdAsync(request.ReaderId);
            Book book = await unitOfWork.BookRepository.GetByISBNAsync(request.BookISBN);

            await emailService.SendEmailAsync($"You must return book {book.Title} {request.ReturnDate}",
                "LibraryNotification", user.Email);

            request.ReaderId = reader.Id;

            takenBook = mapper.Map<ReaderBook>(request);
            await unitOfWork.ReaderBookRepository.AddAsync(takenBook);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
