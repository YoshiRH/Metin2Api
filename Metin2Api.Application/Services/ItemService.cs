using Metin2Api.Application.Dtos;
using Metin2Api.Domain.Repositories;
using Metin2Api.Domain.Entities;

namespace Metin2Api.Application.Services
{
    public class ItemService(IItemRepository itemRepository, ICharacterRepository characterRepository) : IItemService
    {
        private readonly IItemRepository _itemRepository = itemRepository;
        private readonly ICharacterRepository _characterRepository = characterRepository;

        public async Task<string> AddItemToCharacterAsync(CreateItemDto item, int characterId)
        {
            var character = await _characterRepository.GetCharacterByIdAsync(characterId);

            if (character == null)
                return "Character doesn't exist";

            if(item == null)
                return "Item missing";

            var newItem = new Item
            {
                Name = item.Name,
                Value = item.Value,
                CharacterId = character.Id
            };

            var addedItem = await _itemRepository.AddItemToCharacterAsync(characterId, newItem);

            if (addedItem == null)
                return "Failed to add item";

            return "Item added";
        }

        public async Task<IEnumerable<ItemDto>> GetAllItemsAsync()
        {
            var items = await _itemRepository.GetAllItemsAsync();

            if(items == null)
                return Enumerable.Empty<ItemDto>();

            var formatedItems = items
                .Select(i => new ItemDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Value = i.Value,
                    CharacterId = i.CharacterId
                });

            return formatedItems;
        }

        public async Task<ItemDto?> GetItemByIdAsync(int itemId)
        {
            var item = await _itemRepository.GetItemByIdAsync(itemId);

            if(item == null) return null;

            var formatedItem = new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Value = item.Value,
                CharacterId = item.CharacterId
            };

            return formatedItem;
        }
    }
}
