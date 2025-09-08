using Metin2Api.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metin2Api.Infrastructure.Configuration
{
    public class GuildConfiguration
    {
        public void Configure(EntityTypeBuilder<Guild> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.GuildName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.MasterId).IsRequired();
        }
    }
}
