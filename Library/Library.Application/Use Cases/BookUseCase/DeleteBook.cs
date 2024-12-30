using Library.Application.DTO.AuthorDto;
using Library.Application.DTO.BookDto;
using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;

namespace Library.Application.Use_Cases.BookUseCase
{
    public class DeleteBook
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteBook(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(DeleteBookRequest request)
        {
            Book book = await unitOfWork.BookRepository.GetByISBNAsync(request.ISBN);

            if (book == null)
            {
                throw new NotFoundException("Book not found");
            }

            await unitOfWork.BookRepository.DeleteAsync(book);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
