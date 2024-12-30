using Library.Application.DTO.AuthorDto;
using Library.Application.Exceptions;
using AutoMapper;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.Entities;

namespace Library.Application.Use_Cases.AuthorUseCase
{
    public class GetAuthorById
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAuthorById(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<AuthorResponse> ExecuteAsync(GetAuthorByIdRequest request,
            CancellationToken cancellationToken = default)
        {
            Author? author = await unitOfWork.AuthorRepository.GetByIdAsync(request.AuthorId);

            if (author == null)
            {
                throw new NotFoundException("Author not found");
            }
            else
            {
                AuthorResponse response = mapper.Map<AuthorResponse>(author);
                return response;
            }
        }
    }
}