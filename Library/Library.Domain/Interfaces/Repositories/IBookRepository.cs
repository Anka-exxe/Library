using Library.Domain.Entities;

namespace Library.Domain.Interfaces.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<List<Book>> GetBooksByPage(int page, int pageSize);
        Task<Book> GetByISBNAsync(string isbn);
        Task<List<Book>> GetByAuthorIdAsync(Guid id, int page, int pageSize);
    }
}
