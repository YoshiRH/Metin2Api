using Metin2Api.Domain.Enums;


namespace Metin2Api.Domain.Entities
{
    public class Character
    {
        // Character properties
        public int Id { get; set; }
        public string Nick { get; set; } = string.Empty;
        public int Level { get; set; }
        public Kingdom Kingdom { get; set; }
        public CharacterClass Class { get; set; }

        // Foreign key to Account
        public int AccountId { get; set; }
        public Account? Account { get; set; }

        // Foreign key to Guild
        public int? GuildId { get; set; }
        public Guild? Guild { get; set; }

        // Inventory in character
        public List<Item> Items { get; set; } = new();
    }
}
