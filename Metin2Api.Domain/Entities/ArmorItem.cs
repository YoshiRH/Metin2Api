namespace Metin2Api.Domain.Entities
{
    public  class ArmorItem : IItem
    {
        // Item Properties
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DefensePower { get; set; }
        public int RequiredLevel { get; set; }

        public int InventoryId { get; set; }
        public Inventory? Inventory { get; set; }
    }
}
