namespace Metin2Api.Domain.Entities
{
    public class Item
    {
        // Common Item Properties
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Value { get; set; }

        // Foreign key to Inventory
        public int CharacterId { get; set; }
        public Character? Character { get; set; } = null!;
    }
}
