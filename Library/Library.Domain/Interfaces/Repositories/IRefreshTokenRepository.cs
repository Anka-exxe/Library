using Library.Domain.Entities;

namespace Library.Domain.Interfaces.Repositories
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task SaveRefreshTokenAsync(RefreshToken token);
        Task<RefreshToken> GetRefreshTokenAsync(string username, string refreshToken);
        Task<RefreshToken> GetByUsernameAsync(string username);
    }
}