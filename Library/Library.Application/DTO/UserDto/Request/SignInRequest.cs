namespace Library.Application.DTO.UserDto.Request
{
    public class SignInRequest
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
