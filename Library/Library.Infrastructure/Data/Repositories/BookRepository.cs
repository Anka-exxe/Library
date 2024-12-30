using Library.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Library.Domain.Entities;
using System.Threading;

namespace Library.Infrastructure.Data.Repositories
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public new async Task<Book> GetByISBNAsync(string isbn)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.TakenBook)
                .Include(b => b.Image)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.ISBN == isbn);
        }

        public new async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books
                .Include(e => e.TakenBook)
                .Include(e => e.Image)
                .Include(e => e.Author)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Book>> GetByAuthorIdAsync(Guid id, int page, int pageSize)
        {
            return await _context.Books
               .Include(e => e.TakenBook)
               .Include(e => e.Image)
               .Include(e => e.Author)
               .Where(b => b.AuthorId == id)
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
        }

        public async Task<List<Book>> GetBooksByPage(int page, int pageSize)
        {
            return await _context.Books
                .Include(e => e.TakenBook)
                .Include(e => e.Image)
                .Include(e => e.Author)
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
