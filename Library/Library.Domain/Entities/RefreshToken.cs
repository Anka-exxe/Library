﻿namespace Library.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}