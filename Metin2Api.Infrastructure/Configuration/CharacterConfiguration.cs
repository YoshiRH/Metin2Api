using Metin2Api.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metin2Api.Infrastructure.Configuration
{
    public class CharacterConfiguration
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Nick).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Level).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Class).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Kingdom).IsRequired().HasMaxLength(200);

        }
    }
}
