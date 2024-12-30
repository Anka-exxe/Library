using Library.Domain.Interfaces.Repositories;
using Library.Application.DTO.UserDto.Request;
using Library.Application.Interfaces;
using FluentValidation;
using Library.Application.Exceptions;
using Library.Application.DTO.TokenDto;
using Library.Domain.Entities;
using System.Threading;

namespace Library.Application.Use_Cases.UserUseCase
{
    public class Login
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ValidateCredentials _validateCredentialsUseCase;
        private readonly IJwtService _jwtService;
        private readonly IValidationService _validationService;
        private readonly IValidator<SignInRequest> _validator;

        public Login(IUnitOfWork unitOfWork,
            ValidateCredentials validateCredentialsUseCase,
            IJwtService jwtService,
            IValidationService validationService,
            IValidator<SignInRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _validateCredentialsUseCase = validateCredentialsUseCase;
            _validationService = validationService;
            _validator = validator;
        }

        public async Task<TokenResponse> ExecuteAsync(SignInRequest userInfo)
        {
            await _validationService.ValidateAsync(_validator, userInfo);

            var isValid = await _validateCredentialsUseCase.ExecuteAsync(userInfo.Login, userInfo.Password);
            if (isValid)
            {
                var user = await _unitOfWork.UserRepository.GetByUsernameAsync(userInfo.Login);

                TokenResponse token = new TokenResponse
                {
                    AccessToken = _jwtService.GenerateToken(user.Username, user.Role),
                    RefreshToken = _jwtService.GenerateRefreshToken()
                };

                string refreshTokenString = _jwtService.GenerateRefreshToken();

                RefreshToken refreshToken = await _unitOfWork.RefreshTokenRepository.GetByUsernameAsync(user.Username);
                refreshToken.Token = refreshTokenString;
                refreshToken.ExpiryDate = DateTime.Now.AddMinutes(60);
                await _unitOfWork.RefreshTokenRepository.UpdateAsync(refreshToken);
                await _unitOfWork.SaveChangesAsync();

                return token;
            }
            else
            {
                throw new BadRequestException("Invalid username or password");
            }
        }
    }
}
