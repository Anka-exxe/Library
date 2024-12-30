using Library.Domain.Entities;

namespace Library.Domain.Interfaces.Repositories
{
    public interface IReaderBookRepository : IRepository<ReaderBook>
    {
        Task<List<ReaderBook>> GetTakenBooksByReader(Guid readerId, int page, int pageSize);
        Task<ReaderBook> GetByISBNAsync(string isbn);
    }
}
