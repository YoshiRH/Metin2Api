using Metin2Api.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metin2Api.Infrastructure.Configuration
{
    public class ItemConfiguration
    {
        public void Configure(EntityTypeBuilder<IItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Value).IsRequired().HasMaxLength(200);
        }
    }
}
