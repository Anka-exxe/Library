using Library.Domain.Interfaces.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace Library.Application.Use_Cases.UserUseCase
{
    public class ValidateCredentials
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidateCredentials(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(string username, string password)
        {
            var user = await _unitOfWork.UserRepository.GetByUsernameAsync(username);
            if (user == null)
                throw new Exception("Invalid username or password");


            var passwordHash = HashPassword(password);
            return user.PasswordHash == passwordHash;
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
