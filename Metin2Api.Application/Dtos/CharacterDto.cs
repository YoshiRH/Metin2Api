using Metin2Api.Domain.Entities;
using Metin2Api.Domain.Enums;

namespace Metin2Api.Application.Dtos
{
    public class CharacterDto
    {
        // Character properties
        public int Id { get; set; }
        public string Nick { get; set; } = string.Empty;
        public int Level { get; set; }
        public Kingdom Kingdom { get; set; }
        public CharacterClass Class { get; set; }

        // Foreign key to Account
        public int AccountId { get; set; }

        // Foreign key to Guild
        public int? GuildId { get; set; }

    }
}
