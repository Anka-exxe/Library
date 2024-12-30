namespace Library.Application.DTO.BookDto
{
    public class BookInfoResponse
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public Guid? ImageId { get; set; }
        public string ImagePath { get; set; }
    }
}
