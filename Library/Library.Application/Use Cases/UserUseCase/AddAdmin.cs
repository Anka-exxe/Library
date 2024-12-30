using FluentValidation;
using Library.Application.DTO.UserDto.Request;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Use_Cases.UserUseCase
{
    public class AddAdmin
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidationService _validationService;
        private readonly IValidator<AddAdminRequest> _validator;
        private readonly ValidateCredentials _validateCredentialsUseCase;
        private readonly IJwtService _jwtService;

        public AddAdmin(IUnitOfWork unitOfWork,
            IValidationService validationService,
            IValidator<AddAdminRequest> validator,
            ValidateCredentials validateCredentialsUseCase,
            IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _validationService = validationService;
            _validator = validator;
            _validateCredentialsUseCase = validateCredentialsUseCase;
            _jwtService = jwtService;
        }

        public async Task<bool> ExecuteAsync(AddAdminRequest newAdmin)
        {
            await _validationService.ValidateAsync(_validator, newAdmin);

            var passwordHash = _validateCredentialsUseCase.HashPassword(newAdmin.Password);

            if (_unitOfWork.UserRepository.GetByUsernameAsync(newAdmin.Username).Result != null)
            {
                throw new Exception("User with this username already exists");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = newAdmin.Username,
                Email = newAdmin.Email,
                PasswordHash = passwordHash,
                Role = "Admin"
            };

            RefreshToken refreshToken = new RefreshToken()
            {
                Id = Guid.NewGuid(),
                ExpiryDate = DateTime.Now,
                Token = _jwtService.GenerateRefreshToken(),
                Username = user.Username,
            };

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.RefreshTokenRepository.AddAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
