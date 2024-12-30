namespace Library.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IReaderRepository ReaderRepository { get; }
        IBookRepository BookRepository { get; }
        IReaderBookRepository ReaderBookRepository { get; }
        IImageRepository ImageRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        Task SaveChangesAsync();
    }
}
