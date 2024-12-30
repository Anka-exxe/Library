using AutoMapper;
using Library.Application.DTO.BookDto;
using Library.Application.DTO.ReaderBookDto;
using Library.Domain.Entities;

namespace Library.Application.Mappers
{
    public class ReaderBookMapper : Profile
    {
        public ReaderBookMapper()
        {
            CreateMap<GiveABookRequest, ReaderBook>();

            CreateMap<ReaderBook, TakenBookResponse>()
             .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book))
             .ForMember(dest => dest.IsTaken, opt => opt.MapFrom(src => true));

            CreateMap<Book, TakenBookResponse>()
              .ForMember(dest => dest.Book, opt => opt.MapFrom(src => new BookInfoResponse
              {
                  ISBN = src.ISBN,
                  Title = src.Title,
                  Genre = src.Genre,
                  Description = src.Description,
                  AuthorId = src.AuthorId,
                  AuthorName = src.Author != null ? $"{src.Author.Name} {src.Author.Surname}" : "",
                  ImageId = src.Image != null ? src.Image.Id : null,
                  ImagePath = src.Image != null ? src.Image.FilePath : ""
              }))
               .ForMember(dest => dest.IsTaken, opt => opt.MapFrom(src => false));
        }
    }
}
