using Library.Domain.Interfaces.Repositories;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Library.Infrastructure.Data.Repositories
{
    public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
    {
        private readonly LibraryDbContext _context;

        public RefreshTokenRepository(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task SaveRefreshTokenAsync(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(string username, string refreshToken)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Username == username && rt.Token == refreshToken);
        }

        public async Task<RefreshToken> GetByUsernameAsync(string username)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Username == username);
        }
    }
}
