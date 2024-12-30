using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Library.Domain.Entities;

namespace Library.Infrastructure.Data.Configurations
{
    internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.Id);

            builder
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);

            builder
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(a => a.Surname)
                .IsRequired()
                .HasMaxLength(30);

            builder
                .Property(a => a.BirthDate)
                .IsRequired();

            builder
                .Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
