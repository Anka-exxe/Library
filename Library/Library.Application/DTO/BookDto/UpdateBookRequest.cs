namespace Library.Application.DTO.BookDto
{
    public class UpdateBookRequest
    {
        public BookBaseDto Book { get; set; }
        public AddImageRequest Image { get; set; }
    }
}
