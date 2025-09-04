
using Metin2Api.Application.Dtos;

namespace Metin2Api.Application.Services
{
    public class ItemService : IItemService
    {
        public Task AddItemToCharacterAsync(CreateItemDto item, int characterId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItemAsync(ItemDto item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemDto>> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ItemDto?> GetItemByIdAsync(int itemId)
        {
            throw new NotImplementedException();
        }
    }
}
