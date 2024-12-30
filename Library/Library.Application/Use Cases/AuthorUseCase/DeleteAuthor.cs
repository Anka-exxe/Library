using Library.Application.DTO.AuthorDto;
using Library.Application.Exceptions;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.Entities;

namespace Library.Application.Use_Cases.AuthorUseCase
{
    public class DeleteAuthor
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteAuthor(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(DeleteAuthorRequest request)
        {
            Author author = await unitOfWork.AuthorRepository.GetByIdAsync(request.AuthorId);

            if (author == null)
            {
                throw new NotFoundException("Author not found");
            }

            await unitOfWork.AuthorRepository.DeleteAsync(author);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
