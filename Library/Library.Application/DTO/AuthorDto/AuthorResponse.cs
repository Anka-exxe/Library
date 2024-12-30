namespace Library.Application.DTO.AuthorDto
{
    public class AuthorResponse
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required DateTime BirthDate { get; set; }
        public required string Country { get; set; }
    }
}
