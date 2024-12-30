using Library.Application.DTO.AuthorDto;
using Library.Application.Exceptions;
using Library.Application.Interfaces;
using AutoMapper;
using FluentValidation;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.Entities;

namespace Library.Application.Use_Cases.AuthorUseCase
{
    public class UpdateAuthor
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IValidator<UpdateAuthorRequest> validator;
        private readonly IValidationService validationService;

        public UpdateAuthor(IUnitOfWork unitOfWork, 
            IMapper mapper,
            IValidator<UpdateAuthorRequest> validator, 
            IValidationService validationService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validator = validator;
            this.validationService = validationService;
        }

        public async Task ExecuteAsync(UpdateAuthorRequest request)
        {
            await validationService.ValidateAsync(validator, request);

            var existingAuthor = await unitOfWork.AuthorRepository.GetByIdAsync(request.AuthorId);
            if (existingAuthor == null)
            {
                throw new NotFoundException("Author not found");
            }

            mapper.Map(request.Author, existingAuthor);

            await unitOfWork.AuthorRepository.UpdateAsync(existingAuthor);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
