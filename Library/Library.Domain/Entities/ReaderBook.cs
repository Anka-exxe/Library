namespace Library.Domain.Entities
{
    public class ReaderBook
    {
        public Guid Id { get; set; }
        public Reader Reader { get; set; }
        public Guid ReaderId { get; set; }
        public Book Book { get; set; }
        public string BookISBN { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
