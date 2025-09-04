using Metin2Api.Application.Dtos;
using Metin2Api.Domain.Entities;

namespace Metin2Api.Application.Services
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDto>> GetAllItemsAsync();
        Task<ItemDto?> GetItemByIdAsync(int itemId);
        Task AddItemToCharacterAsync(CreateItemDto item, int characterId);
        Task DeleteItemAsync(ItemDto item);
    }
}
