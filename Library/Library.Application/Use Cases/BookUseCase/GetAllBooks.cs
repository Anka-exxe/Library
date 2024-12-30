using AutoMapper;
using Library.Application.DTO.BookDto;
using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;

namespace Library.Application.Use_Cases.BookUseCase
{
    public class GetAllBooks
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllBooks(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BookInfoResponse>> ExecuteAsync(int pageNumber, int pageSize)
        {
            IEnumerable<Book>? books = await unitOfWork.BookRepository
                .GetBooksByPage(pageNumber, pageSize);

            if (!books.Any())
            {
                throw new NotFoundException("Books not found");
            }
            else
            {
                IEnumerable<BookInfoResponse> response = mapper.Map<IEnumerable<BookInfoResponse>>(books);
                return response;
            }
        }
    }
}