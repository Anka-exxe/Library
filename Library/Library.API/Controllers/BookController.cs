using Library.Application.DTO.BookDto;
using Library.Application.Use_Cases.BookUseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private AddBook addBookUseCase;
        private GetAllBooks getAllBooksUseCase;
        private GetBooksByAuthorId getBooksByAuthorIdUseCase;
        private GetBookByISBN getBookByISBNUseCase;
        private DeleteBook deleteBookUseCase;
        private UpdateBook updateBookUseCase;

        public BookController(AddBook addBookUseCase,
            GetAllBooks getAllBooksUseCase,
            GetBooksByAuthorId getBooksByAuthorIdUseCase,
            GetBookByISBN getBookByISBNUseCase,
            DeleteBook deleteBookUseCase,
            UpdateBook updateBookUseCase
            )
        {
            this.addBookUseCase = addBookUseCase;
            this.deleteBookUseCase = deleteBookUseCase;
            this.getAllBooksUseCase = getAllBooksUseCase;
            this.getBooksByAuthorIdUseCase = getBooksByAuthorIdUseCase;
            this.getBookByISBNUseCase = getBookByISBNUseCase;
            this.updateBookUseCase = updateBookUseCase;
        }

        [HttpPost("addBook")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AddBook([FromForm] AddBookRequest request)
        {
            await addBookUseCase.ExecuteAsync(request);
            return Ok();
        }

        [HttpGet("allBook")]
        public async Task<IActionResult> GetAllBooks(int pageNumber)
        {
            var books = await getAllBooksUseCase.ExecuteAsync(pageNumber, 9);
            return Ok(books);
        }

        [HttpGet("authorsBook")]
        public async Task<IActionResult> GetBooksByAuthor(Guid authorId, int pageNumber)
        {
            var books = await getBooksByAuthorIdUseCase.ExecuteAsync(authorId, pageNumber, 9);
            return Ok(books);
        }

        [HttpGet("getByISBN")]
        public async Task<IActionResult> GetBooksByAuthor(string ISBN)
        {
            var book = await getBookByISBNUseCase.ExecuteAsync(ISBN);
            return Ok(book);
        }

        [HttpDelete("deleteBook")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteBook([FromQuery] DeleteBookRequest request)
        {
            await deleteBookUseCase.ExecuteAsync(request);
            return Ok("Book deleted successfully");
        }

        [HttpPut("updateBook")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateBook([FromForm] UpdateBookRequest request)
        {
            await updateBookUseCase.ExecuteAsync(request);
            return Ok();
        }
    }
}
