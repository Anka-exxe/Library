namespace Library.Application.DTO.ReaderBookDto
{
    public class GiveABookRequest
    {
        public Guid ReaderId { get; set; }
        public string BookISBN { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
