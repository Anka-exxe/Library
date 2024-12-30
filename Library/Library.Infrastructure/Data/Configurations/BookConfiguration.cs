using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Library.Domain.Entities;

namespace Library.Infrastructure.Data.Configurations
{
    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.ISBN);

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(b => b.Genre)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(b => b.Author)
               .WithMany(a => a.Books);

            builder.HasOne(b => b.TakenBook)
                .WithOne(rb => rb.Book).OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
