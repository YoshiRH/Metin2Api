using Metin2Api.Application.Dtos;
using Metin2Api.Domain.Entities;

namespace Metin2Api.Application.Services
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDto>> GetAllItemsAsync();
        Task<ItemDto?> GetItemByIdAsync(int itemId);
        Task<string> AddItemToCharacterAsync(CreateItemDto item, int characterId);
    }
}
