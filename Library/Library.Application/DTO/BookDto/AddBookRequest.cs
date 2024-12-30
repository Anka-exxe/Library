namespace Library.Application.DTO.BookDto
{
    public class AddBookRequest
    {
        public BookBaseDto Book { get; set; }
        public AddImageRequest? Image { get; set; } = null;
    }
}
