using Metin2Api.Domain.Enums;

namespace Metin2Api.Application.Dtos
{
    public class CreateCharacterDto
    {
        // Character properties
        public string Nick { get; set; } = string.Empty;
        public Kingdom Kingdom { get; set; }
        public CharacterClass Class { get; set; }

        // Foreign key to Account
        public int AccountId { get; set; }

    }
}
