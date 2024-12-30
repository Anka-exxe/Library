using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Library.Domain.Entities;

namespace Library.Infrastructure.Data.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.FilePath)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(i => i.Book)
                .WithOne(b => b.Image);
        }
    }
}
