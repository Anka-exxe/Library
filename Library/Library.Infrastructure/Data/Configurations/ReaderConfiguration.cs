using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Library.Domain.Entities;

namespace Library.Infrastructure.Data.Configurations
{
    internal class ReaderConfiguration : IEntityTypeConfiguration<Reader>
    {
        public void Configure(EntityTypeBuilder<Reader> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.BirthDate)
                .IsRequired();

            builder.HasOne(r => r.User)
               .WithOne(u => u.Reader)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.TakenBooks)
                .WithOne(rb => rb.Reader)
                .HasForeignKey(rb => rb.ReaderId);
        }
    }
}
