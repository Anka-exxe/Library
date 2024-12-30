using Library.Application.DTO.AuthorDto;
using AutoMapper;
using Library.Domain.Entities;

namespace Library.Application.Mappers
{
    public class AuthorMapper : Profile
    {
        public AuthorMapper()
        {
            CreateMap<Author, AuthorResponse>();
            CreateMap<AuthorBaseDTO, Author>();
            CreateMap<IEnumerable<Author>, IEnumerable<AuthorResponse>>()
                .ConvertUsing(authors => authors.Select(a => new AuthorResponse
                {
                    Id = a.Id,
                    Name = a.Name,
                    Surname = a.Surname,
                    Country = a.Country,
                    BirthDate = a.BirthDate
                }));
        }
    }
}
