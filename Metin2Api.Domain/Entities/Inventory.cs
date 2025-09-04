namespace Metin2Api.Domain.Entities
{
    public class Inventory
    {
        // Inventory properties
        public int Id { get; set; }
        public List<IItem> Items { get; set; } = new();

        // Foreign key to Character
        public int CharacterId { get; set; }
        public Character? Character { get; set; }
    }
}
