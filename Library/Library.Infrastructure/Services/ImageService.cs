using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Library.Application.Interfaces;

namespace Library.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly string imageDirectory;

        public ImageService(IConfiguration configuration)
        {
            imageDirectory = configuration["ImageDirectory"];
        }

        public async Task<string> SaveImageAsync(IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return Path.Combine(imageDirectory, "defaultImage"); 
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(imageDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}
