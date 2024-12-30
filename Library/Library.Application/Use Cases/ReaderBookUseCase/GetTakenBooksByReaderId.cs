using AutoMapper;
using Library.Application.DTO.BookDto;
using Library.Application.DTO.ReaderBookDto;
using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;

namespace Library.Application.Use_Cases.ReaderBookUseCase
{
    public class GetTakenBooksByReaderId
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetTakenBooksByReaderId(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TakenBookResponse>> ExecuteAsync(Guid readerId, int pageNumber, int pageSize)
        {
            Reader reader = await unitOfWork.ReaderRepository.GetByUserIdAsync(readerId);

            if (reader == null)
            {
                throw new NotFoundException("Reader not found");
            }

            IEnumerable<ReaderBook>? takenBooks = await unitOfWork.ReaderBookRepository
                .GetTakenBooksByReader(reader.Id, pageNumber, pageSize);

            foreach (var book in takenBooks)
            {
                book.Book = await unitOfWork.BookRepository.GetByISBNAsync(book.BookISBN);
            }

            if (!takenBooks.Any())
            {
                throw new NotFoundException("The user did not take the books");
            }
            else
            {
                IEnumerable<TakenBookResponse> response = mapper.Map<IEnumerable<TakenBookResponse>>(takenBooks);
                return response;
            }
        }
    }
}
