using Metin2Api.Domain.Entities;

namespace Metin2Api.Domain.Repositories
{
    public interface IItemRepository
    {
        Task<IEnumerable<IItem>> GetAllItemsAsync();
        Task<IItem?> GetItemByIdAsync(int itemId);
        Task AddItemAsync(IItem item);
        Task<IItem?> AddItemToCharacterAsync(int charactedId, IItem item);
        Task SaveChangesAsync();
    }
}
