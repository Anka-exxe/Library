using AutoMapper;
using Library.Application.DTO.BookDto;
using Library.Application.DTO.ReaderBookDto;
using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;

namespace Library.Application.Use_Cases.ReaderBookUseCase
{
    public class GetAllBooksWithTakenInfo
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllBooksWithTakenInfo(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TakenBookResponse>> ExecuteAsync()
        {
            IEnumerable<ReaderBook>? takenBooks = await unitOfWork.ReaderBookRepository.GetAllAsync();
            IEnumerable<Book>? books = await unitOfWork.BookRepository.GetAllAsync();

            if (books == null || !books.Any())
            {
                throw new NotFoundException("Books not found");
            }

            IEnumerable<TakenBookResponse> response = mapper.Map<IEnumerable<TakenBookResponse>>(books);

            foreach (var takenBook in takenBooks)
            {
                var bookResponse = response.FirstOrDefault(b => b.Book.ISBN == takenBook.BookISBN);
                if (bookResponse != null)
                {
                    bookResponse.IsTaken = true;
                }
            }

            return response;
        }
    }
}
