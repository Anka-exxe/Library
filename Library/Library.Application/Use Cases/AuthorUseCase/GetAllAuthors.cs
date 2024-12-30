using Library.Application.DTO.AuthorDto;
using Library.Application.Exceptions;
using AutoMapper;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.Entities;

namespace Library.Application.Use_Cases.AuthorUseCase
{
    public class GetAllAuthors
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllAuthors(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<AuthorResponse>> ExecuteAsync(int pageNumber, int pageSize)
        {
            IEnumerable<Author>? authors = await unitOfWork.AuthorRepository.GetAllAsync(pageNumber, pageSize);

            if (!authors.Any())
            {
                throw new NotFoundException("Authors not found");
            }
            else
            {
                IEnumerable<AuthorResponse> response = mapper.Map<IEnumerable<AuthorResponse>>(authors);
                return response;
            }
        }
    }
}
