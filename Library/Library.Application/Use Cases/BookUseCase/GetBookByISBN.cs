using AutoMapper;
using Library.Application.DTO.BookDto;
using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;

namespace Library.Application.Use_Cases.BookUseCase
{
    public class GetBookByISBN
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetBookByISBN(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BookInfoResponse> ExecuteAsync(string ISBN)
        {
            Book? book = await unitOfWork.BookRepository.GetByISBNAsync(ISBN);
            if (book == null)
            {
                throw new NotFoundException("Book not found");
            }
            else
            {
                BookInfoResponse response = mapper.Map<BookInfoResponse>(book);
                return response;
            }
        }
    }
}
