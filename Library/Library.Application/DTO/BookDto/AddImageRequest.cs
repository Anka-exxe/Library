using Microsoft.AspNetCore.Http;

namespace Library.Application.DTO.BookDto
{
    public class AddImageRequest
    {
        public required IFormFile? ImageFile { get; set; } = null;
    }
}
