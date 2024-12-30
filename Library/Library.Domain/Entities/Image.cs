namespace Library.Domain.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        public string FilePath { get; set; }
        public string? BookISBN { get; set; }
        public Book Book { get; set; }
    }
}
