using AutoMapper;
using Library.Application.DTO.BookDto;
using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Use_Cases.BookUseCase
{
    public class GetBooksByAuthorId
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetBooksByAuthorId(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BookInfoResponse>> ExecuteAsync(Guid authorId, int pageNumber, int pageSize)
        {
            IEnumerable<Book>? books = await unitOfWork.BookRepository
                .GetByAuthorIdAsync(authorId, pageNumber, pageSize);

            if (!books.Any())
            {
                throw new NotFoundException("Author has not books");
            }
            else
            {
                IEnumerable<BookInfoResponse> response = mapper.Map<IEnumerable<BookInfoResponse>>(books);
                return response;
            }
        }
    }
}
