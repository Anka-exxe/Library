using Microsoft.EntityFrameworkCore;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Data.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Token)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(t => t.ExpiryDate)
                .IsRequired();
        }
    }
}
