namespace Library.Domain.Entities
{
    public class Reader
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<ReaderBook>? TakenBooks { get; set; } = new List<ReaderBook>();
    }
}
