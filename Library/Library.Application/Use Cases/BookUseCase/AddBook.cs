using AutoMapper;
using FluentValidation;
using Library.Application.DTO.BookDto;
using Library.Application.Exceptions;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;

namespace Library.Application.Use_Cases.BookUseCase
{
    public class AddBook
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IValidator<AddBookRequest> validator;
        private readonly IValidationService validationService;
        private readonly IImageService imageService;

        public AddBook(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<AddBookRequest> validator, IValidationService validationService,
            IImageService imageService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validator = validator;
            this.validationService = validationService;
            this.imageService = imageService;
        }

        public async Task ExecuteAsync(AddBookRequest request)
        {
            await validationService.ValidateAsync(validator, request);

            if (await unitOfWork.AuthorRepository.GetByIdAsync(request.Book.AuthorId) == null)
            {
                throw new NotFoundException("Author not found");
            }
            if (await unitOfWork.BookRepository.GetByISBNAsync(request.Book.ISBN) != null)
            {
                throw new NotFoundException("Book with this ISBN already exists");
            }

            string filePath = await imageService.SaveImageAsync(request.Image.ImageFile);

            Book book = mapper.Map<Book>(request.Book);
            await unitOfWork.BookRepository.AddAsync(book);

            var image = new Image
            {
                Id = new Guid(),
                BookISBN = request.Book.ISBN,
                Book = book,
                FilePath = filePath
            };

            await unitOfWork.ImageRepository.AddAsync(image);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
