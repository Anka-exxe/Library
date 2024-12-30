namespace Library.Application.DTO.AuthorDto
{
    public class UpdateAuthorRequest
    {
        public Guid AuthorId { get; set; }
        public AuthorBaseDTO Author { get; set; }
    }
}
