using Metin2Api.Domain.Entities;

namespace Metin2Api.Domain.Repositories
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item?> GetItemByIdAsync(int itemId);
        Task<Item?> AddItemToCharacterAsync(int charactedId, Item item);
        Task SaveChangesAsync();
    }
}
