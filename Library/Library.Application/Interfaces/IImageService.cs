using Microsoft.AspNetCore.Http;

namespace Library.Application.Interfaces
{
    public interface IImageService
    {
        public Task<string> SaveImageAsync(IFormFile imageFile);
    }
}
