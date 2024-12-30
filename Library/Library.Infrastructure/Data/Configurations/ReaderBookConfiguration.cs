using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Library.Domain.Entities;

namespace Library.Infrastructure.Data.Configurations
{
    internal class ReaderBookConfiguration : IEntityTypeConfiguration<ReaderBook>
    {
        public void Configure(EntityTypeBuilder<ReaderBook> builder)
        {
            builder.HasKey(rb => rb.Id);

            builder
             .HasOne(rb => rb.Book)
             .WithOne(b => b.TakenBook)
             .HasForeignKey<ReaderBook>(rb => rb.BookISBN);

            builder.HasOne(rb => rb.Reader)
                .WithMany(r => r.TakenBooks)
                .HasForeignKey(rb => rb.ReaderId);

            builder.Property(rb => rb.ReceiptDate)
                .IsRequired();

            builder.Property(rb => rb.ReturnDate)
                .IsRequired();
        }
    }
}
