namespace Metin2Api.Domain.Entities
{
    public interface IItem
    {
        // Common Item Properties
        public int Id { get; set; }
        public string Name { get; set; }

        // Foreign key to Inventory
        public int InventoryId { get; set; }
        public Inventory? Inventory { get; set; }
    }
}
