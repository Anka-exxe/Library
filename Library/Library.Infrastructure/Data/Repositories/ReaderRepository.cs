using Library.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Library.Domain.Entities;

namespace Library.Infrastructure.Data.Repositories
{
    public class ReaderRepository : RepositoryBase<Reader>, IReaderRepository
    {
        private readonly LibraryDbContext _context;

        public ReaderRepository(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public new async Task<IEnumerable<Reader>> GetAllAsync()
        {
            return await _context.Readers
                .Include(r => r.TakenBooks)
                .AsNoTracking()
                .ToListAsync();
        }

        public new async Task<Reader> GetByIdAsync(Guid id)
        {
            return await _context.Readers
                .Include(r => r.TakenBooks)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Reader> GetByUserIdAsync(Guid userId)
        {
            return await _context.Readers
                .Include(r => r.TakenBooks)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.UserId == userId);
        }
    }
}
