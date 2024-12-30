using Library.Application.DTO.AuthorDto;
using Library.Application.DTO.ReaderBookDto;
using Library.Application.Use_Cases.ReaderBookUseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReaderBookController : ControllerBase
    {
        private GiveABook giveABookUseCase;
        private ReturnBook returnBookUseCase;
        private GetTakenBooksByReaderId getTakenBooksByReaderIdUseCase;
        private GetAllBooksWithTakenInfo getAllBooksWithTakenInfoUseCase;

        public ReaderBookController(GiveABook giveABookUseCase, ReturnBook returnBookUseCase,
            GetTakenBooksByReaderId getTakenBooksByReaderIdUseCase, GetAllBooksWithTakenInfo getAllBooksWithTakenInfoUseCase)
        {
            this.giveABookUseCase = giveABookUseCase;
            this.returnBookUseCase = returnBookUseCase;
            this.getTakenBooksByReaderIdUseCase = getTakenBooksByReaderIdUseCase;
            this.getAllBooksWithTakenInfoUseCase = getAllBooksWithTakenInfoUseCase;
        }


        [HttpPut("giveABook")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GiveABook([FromBody] GiveABookRequest request)
        {
            await giveABookUseCase.ExecuteAsync(request);
            return Ok();
        }

        [HttpPost("returnBook")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookRequest request)
        {
            await returnBookUseCase.ExecuteAsync(request);
            return Ok();
        }

        [HttpGet("getReaderTakenBooks")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetRederTakenBooks(Guid readerId, int pageNumber)
        {
            var books = await getTakenBooksByReaderIdUseCase.ExecuteAsync(readerId, pageNumber, 5);
            return Ok(books);
        }

        [HttpGet("getBooksWithInfoOfTaken")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetBookWithTakenInfos()
        {
            var books = await getAllBooksWithTakenInfoUseCase.ExecuteAsync();
            return Ok(books);
        }
    }
}
