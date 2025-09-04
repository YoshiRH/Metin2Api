using Metin2Api.Domain.Entities;

namespace Metin2Api.Application.Dtos
{
    public class ItemDto
    {
        // Common Item Properties
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Value { get; set; }

        // Foreign key to Inventory
        public int InventoryId { get; set; }
    }
}
