using AutoMapper;
using FluentValidation;
using Library.Application.DTO.BookDto;
using Library.Application.Exceptions;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;

namespace Library.Application.Use_Cases.BookUseCase
{
    public class UpdateBook
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IValidator<UpdateBookRequest> validator;
        private readonly IValidationService validationService;
        private readonly IImageService imageService;

        public UpdateBook(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<UpdateBookRequest> validator, IValidationService validationService,
            IImageService imageService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validator = validator;
            this.validationService = validationService;
            this.imageService = imageService;
        }

        public async Task ExecuteAsync(UpdateBookRequest request)
        {
            await validationService.ValidateAsync(validator, request);

            var author = await unitOfWork.AuthorRepository.GetByIdAsync(request.Book.AuthorId);
            if (author == null)
            {
                throw new NotFoundException("Author not found");
            }

            var existingBook = await unitOfWork.BookRepository.GetByISBNAsync(request.Book.ISBN);
            if (existingBook == null)
            {
                throw new NotFoundException("Book not found");
            }

            mapper.Map(request.Book, existingBook);
            await unitOfWork.BookRepository.UpdateAsync(existingBook);

            if (request.Image?.ImageFile != null)
            {
                Image image = await unitOfWork.ImageRepository.GetByBookIdAsync(existingBook.ISBN);

                if (image == null)
                {
                    image = new Image
                    {
                        Id = Guid.NewGuid(),
                        BookISBN = existingBook.ISBN,
                        Book = existingBook
                    };
                }

                string filePath = await imageService.SaveImageAsync(request.Image.ImageFile);
                image.FilePath = filePath;

                await unitOfWork.ImageRepository.UpdateAsync(image);
            }

            await unitOfWork.SaveChangesAsync();
        }
    }
}