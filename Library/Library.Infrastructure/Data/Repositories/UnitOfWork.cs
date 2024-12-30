using Microsoft.EntityFrameworkCore;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;

namespace Library.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;

        public IUserRepository UserRepository { get; }
        public IAuthorRepository AuthorRepository { get; }
        public IBookRepository BookRepository { get; }
        public IReaderRepository ReaderRepository { get; }
        public IReaderBookRepository ReaderBookRepository { get; }
        public IImageRepository ImageRepository { get; }
        public IRefreshTokenRepository RefreshTokenRepository { get; }

        public UnitOfWork(
            LibraryDbContext context, 
            IUserRepository userRepository,
            IAuthorRepository authorRepository,
            IBookRepository bookRepository,
            IReaderRepository readerRepository,
            IReaderBookRepository readerBookRepository,
            IImageRepository imageRepository,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _context = context;
            UserRepository = userRepository;
            AuthorRepository = authorRepository;
            BookRepository = bookRepository;
            ReaderRepository = readerRepository;
            ReaderBookRepository = readerBookRepository;
            ImageRepository = imageRepository;
            RefreshTokenRepository = refreshTokenRepository;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
