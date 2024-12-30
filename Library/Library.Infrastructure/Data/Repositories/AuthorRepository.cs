using Microsoft.EntityFrameworkCore;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;

namespace Library.Infrastructure.Data.Repositories
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        private readonly LibraryDbContext _context;

        public AuthorRepository(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public new async Task<Author> GetByIdAsync(Guid id)
        {
            return await _context.Authors.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
