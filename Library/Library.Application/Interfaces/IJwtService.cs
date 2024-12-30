using Library.Application.DTO.TokenDto;
using System.Security.Claims;

namespace Library.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string username, string role);
        Task<(string newAccessToken, string newRefreshToken)> GetRefreshToken(TokenResponse tokenRequest);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Task<bool> ValidateRefreshToken(string username, string refreshToken);
        Task SaveRefreshToken(string username, string refreshToken);
        string GenerateRefreshToken();
    }
}
