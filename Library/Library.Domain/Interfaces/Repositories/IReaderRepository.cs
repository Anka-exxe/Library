using Library.Domain.Entities;

namespace Library.Domain.Interfaces.Repositories
{
    public interface IReaderRepository : IRepository<Reader>
    {
        Task<Reader> GetByUserIdAsync(Guid userId);
    }
}
