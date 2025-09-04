namespace Metin2Api.Domain.Entities
{
    public class IItem
    {
        // Common Item Properties
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Value { get; set; }

        // Foreign key to Inventory
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; } = null!;
    }
}
