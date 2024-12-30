using AutoMapper;
using FluentValidation;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Application.DTO.AuthorDto;
using Library.Application.Interfaces;

namespace Library.Application.Use_Cases.AuthorUseCase
{
    public class AddAuthor
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IValidator<AddAuthorRequest> validator;
        private readonly IValidationService validationService;

        public AddAuthor(IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<AddAuthorRequest> validator, IValidationService validationService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validator = validator;
            this.validationService = validationService;
        }

        public async Task ExecuteAsync(AddAuthorRequest request)
        {
            await validationService.ValidateAsync(validator, request);

            Author author = mapper.Map<Author>(request.Author);
            author.Id = Guid.NewGuid();

            await unitOfWork.AuthorRepository.AddAsync(author);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
