using AutoMapper;
using Library.Domain.Entities;
using Library.Application.DTO.BookDto;
using Library.Application.DTO.ReaderBookDto;

namespace Library.Application.Mappers
{
    public class BookMapper : Profile
    {
        public BookMapper()
        {
            CreateMap<BookBaseDto, Book>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<Book, BookInfoResponse>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(
                    src => src.Author != null ? $"{src.Author.Name} {src.Author.Surname}" : string.Empty)
                )
                .ForMember(dest => dest.ImageId, opt => opt.MapFrom(
                    src => src.Image != null ? src.Image.Id : (Guid?)null)
                )
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(
                    src => src.Image != null ? src.Image.FilePath : string.Empty)
                );

            CreateMap<Book, BookBaseDto>();

            CreateMap<IEnumerable<Book>, IEnumerable<BookInfoResponse>>()
                .ConvertUsing(books => books.Select(b => new BookInfoResponse
                {
                    ISBN = b.ISBN,
                    Title = b.Title,
                    Genre = b.Genre,
                    Description = b.Description,
                    AuthorId = b.AuthorId,
                    AuthorName = b.Author.Name + " " + b.Author.Surname,
                    ImageId = b.Image.Id,
                    ImagePath = b.Image.FilePath
                }));



        }
    }
}
