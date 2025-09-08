using Metin2Api.Domain.Entities;

namespace Metin2Api.Application.Dtos
{
    public class GuildDto
    {
        // Guild properties
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int MasterId { get; set; }

        public List<int> MemberIds { get; set; } = new();
    }
}
