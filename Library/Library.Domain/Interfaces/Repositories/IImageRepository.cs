using Library.Domain.Entities;

namespace Library.Domain.Interfaces.Repositories
{
    public interface IImageRepository : IRepository<Image>
    {
        Task<Image> GetByBookIdAsync(string bookISBN);
    }
}
