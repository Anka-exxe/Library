using Library.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Library.Domain.Entities;

namespace Library.Infrastructure.Data.Repositories
{
    public class ImageRepository : RepositoryBase<Image>, IImageRepository
    {
        private readonly LibraryDbContext _context;
        public ImageRepository(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Image> GetByBookIdAsync(string bookISBN)
        {
            return await _context.Images
                .FirstOrDefaultAsync(i => i.BookISBN == bookISBN);
        }
    }
}
