﻿namespace Library.Domain.Entities
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public Author Author { get; set; }
        public Guid AuthorId { get; set; }
        public ReaderBook? TakenBook { get; set; }
        public Image Image { get; set; }
    }
}
