using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using System.Security.Cryptography;
using System.Text;
using Library.Application.DTO.UserDto.Request;
using Library.Application.Interfaces;
using FluentValidation;
using System.Threading;

namespace Library.Application.Use_Cases.UserUseCase
{
    public class RegisterUser
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidationService _validationService;
        private readonly IValidator<RegistrationRequest> _validator;
        private readonly ValidateCredentials _validateCredentialsUseCase;
        private readonly IJwtService _jwtService;

        public RegisterUser(IUnitOfWork unitOfWork, 
            IValidationService validationService, 
            IValidator<RegistrationRequest> validator,
            ValidateCredentials validateCredentialsUseCase,
            IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _validationService = validationService;
            _validator = validator;
            _validateCredentialsUseCase = validateCredentialsUseCase;
            _jwtService = jwtService;
        }

        public async Task<bool> ExecuteAsync(RegistrationRequest newReader)
        {
            await _validationService.ValidateAsync(_validator, newReader);

            var passwordHash = _validateCredentialsUseCase.HashPassword(newReader.Password);

            if (_unitOfWork.UserRepository.GetByUsernameAsync(newReader.Username).Result != null)
            {
                throw new Exception("User with this username already exists");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = newReader.Username,
                Email = newReader.Email,
                PasswordHash = passwordHash,
                Role = "Reader"
            };

            var reader = new Reader
            {
                Id = Guid.NewGuid(),
                FirstName = newReader.FirstName,
                LastName = newReader.LastName,
                BirthDate = newReader.BirthDate,
                UserId = user.Id,
            };

            RefreshToken refreshToken = new RefreshToken()
            {
                Id = Guid.NewGuid(),
                ExpiryDate = DateTime.Now,
                Token = _jwtService.GenerateRefreshToken(),
                Username = user.Username,
            };


            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.ReaderRepository.AddAsync(reader);
            await _unitOfWork.RefreshTokenRepository.AddAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        //private string HashPassword(string password)
        //{
        //    using (var sha256 = SHA256.Create())
        //    {
        //        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        //        var builder = new StringBuilder();
        //        foreach (var b in bytes)
        //        {
        //            builder.Append(b.ToString("x2"));
        //        }
        //        return builder.ToString();
        //    }
        //}
    }
}
