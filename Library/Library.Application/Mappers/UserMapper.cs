using AutoMapper;
using Library.Application.DTO.AuthorDto;
using Library.Domain.Entities;
using Library.Application.DTO.UserDto.Response;

namespace Library.Application.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserResponse>();
            CreateMap<IEnumerable<User>, IEnumerable<UserResponse>>()
               .ConvertUsing(authors => authors.Select(u => new UserResponse
               {
                   Id = u.Id,
                   Username = u.Username,
                   PasswordHash = u.PasswordHash,
                   Email = u.Email,
                   Role = u.Role
               }));
        }
    }
}
