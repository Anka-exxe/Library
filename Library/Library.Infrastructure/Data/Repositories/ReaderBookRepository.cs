using Microsoft.EntityFrameworkCore;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;

namespace Library.Infrastructure.Data.Repositories
{
    public class ReaderBookRepository : RepositoryBase<ReaderBook>, IReaderBookRepository
    {
        private readonly LibraryDbContext _context;

        public ReaderBookRepository(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public new async Task<ReaderBook> GetByIdAsync(Guid id)
        {
            return await _context.ReaderBooks
                .Include(rb => rb.Reader)
                .Include(rb => rb.Book)
                .FirstOrDefaultAsync(rb => rb.Id == id);
        }

        public async Task<ReaderBook> GetByISBNAsync(string isbn)
        {
            return await _context.ReaderBooks
                .Include(rb => rb.Reader)
                .Include(rb => rb.Book)
                .FirstOrDefaultAsync(rb => rb.BookISBN == isbn);
        }

        public new async Task<IEnumerable<ReaderBook>> GetAllAsync()
        {
            return await _context.ReaderBooks
                .Include(rb => rb.Reader)
                .Include(rb => rb.Book)
                .ToListAsync();
        }

        public async Task<List<ReaderBook>> GetTakenBooksByReader(Guid readerId, int page, int pageSize)
        {
            return await _context.ReaderBooks
                .Include(rb => rb.Book)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Where(rb => rb.ReaderId == readerId)
                .ToListAsync();
        }
    }
}
