using Library.Application.DTO.BookDto;

namespace Library.Application.DTO.ReaderBookDto
{
    public class TakenBookResponse
    {
        public BookInfoResponse Book {  get; set; }
        public bool IsTaken { get; set; }
    }
}
