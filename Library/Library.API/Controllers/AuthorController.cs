using Library.Application.DTO.AuthorDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Library.Application.Use_Cases.AuthorUseCase;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : Controller
    {
        private AddAuthor addAuthorUseCase;
        private DeleteAuthor deleteAuthorUseCase;
        private GetAllAuthors getAllAuthorsUseCase;
        private GetAuthorById getAuthorByIdUseCase;
        private UpdateAuthor updateAuthorUseCase;

        public AuthorController(AddAuthor addAuthorUseCase, DeleteAuthor deleteAuthorUseCase,
            GetAllAuthors getAllAuthorsUseCase, GetAuthorById getAuthorByIdUseCase,
            UpdateAuthor updateAuthorUseCase)
        {
            this.addAuthorUseCase = addAuthorUseCase;
            this.deleteAuthorUseCase = deleteAuthorUseCase;
            this.getAllAuthorsUseCase = getAllAuthorsUseCase;
            this.getAuthorByIdUseCase = getAuthorByIdUseCase;
            this.updateAuthorUseCase = updateAuthorUseCase;
        }

        [HttpPost("addAuthor")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorRequest request)
        {
            await addAuthorUseCase.ExecuteAsync(request);
            return Ok();
        }

        [HttpDelete("deleteAuthor")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteAuthor([FromQuery] DeleteAuthorRequest request)
        {
            await deleteAuthorUseCase.ExecuteAsync(request);
            return Ok();
        }

        [HttpGet("getAllAuthors")]
        public async Task<IActionResult> GetAllAuthors(int pageNumber)
        {
            IEnumerable<AuthorResponse> response = await getAllAuthorsUseCase.ExecuteAsync(pageNumber, 5);
            return Ok(response);
        }

        [HttpGet("getAuthorById")]
        public async Task<IActionResult> GetAuthorById([FromQuery] GetAuthorByIdRequest request)
        {
            AuthorResponse response = await getAuthorByIdUseCase.ExecuteAsync(request);
            return Ok(response);
        }

        [HttpPut("updateAuthor")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorRequest request)
        {
            await updateAuthorUseCase.ExecuteAsync(request);
            return Ok();
        }
    }
}
