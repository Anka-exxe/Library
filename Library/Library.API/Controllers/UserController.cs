using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Library.Application.Use_Cases.UserUseCase;
using Library.Application.DTO.UserDto.Request;
using Library.Application.Interfaces;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly RegisterUser _registerUserUseCase;
        private readonly ValidateCredentials _validateCredentialsUseCase;
        private readonly Login _loginUserUseCase;
        private readonly AddAdmin _addAdminUseCase;
        private readonly GetAllUsers _getAllUsersUseCase;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public UserController(
                RegisterUser registerUserUseCase,
                ValidateCredentials validateCredentialsUseCase,
                IMapper mapper,
                IJwtService jwtService,
                Login loginUserUseCase,
                AddAdmin addAdminUseCase,
                GetAllUsers getAllUsersUseCase)
        {
            _registerUserUseCase = registerUserUseCase;
            _validateCredentialsUseCase = validateCredentialsUseCase;
            _mapper = mapper;
            _jwtService = jwtService;
            _loginUserUseCase = loginUserUseCase;
            _addAdminUseCase = addAdminUseCase;
            _getAllUsersUseCase = getAllUsersUseCase;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest registrationModel)
        {
            var result = await _registerUserUseCase.ExecuteAsync(registrationModel);

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] SignInRequest signInModel)
        {
            var token = await _loginUserUseCase.ExecuteAsync(signInModel);

            return Ok(new { Token = token });
        }

        [HttpPost("addAdmin")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] AddAdminRequest addAdminModel)
        {
            var result = await _addAdminUseCase.ExecuteAsync(addAdminModel);

            return Ok("Admin added succesfully");
        }

        [HttpGet("allUsers")]
        //[Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Register()
        {
            var users = await _getAllUsersUseCase.ExecuteAsync();

            return Ok(users);
        }
    }
}
