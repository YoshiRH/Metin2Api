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

            IItem newItem = item.Type.ToLower() switch
            {
                "weapon" => new WeaponItem { AttackPower = item.Value, Name = item.Name },
                "armor" => new ArmorItem { DefensePower = item.Value, Name = item.Name },
                _ => throw new Exception("Invalid Item type")
            };

            character.Inventory.Items.Add(newItem);
            await _itemRepository.AddItemAsync(newItem);

            await _itemRepository.SaveChangesAsync();

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
                    Type = GetType(i),
                    Value = GetValue(i),
                    InventoryId = i.InventoryId
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
                Type = GetType(item),
                Value = GetValue(item),
                InventoryId = item.InventoryId
            };

            return formatedItem;
        }

        private static int GetValue(IItem item) => item switch
        {
            WeaponItem w => w.AttackPower,
            ArmorItem a => a.DefensePower,
            _ => 0
        };
        private static string GetType(IItem item) => item switch
        {
            WeaponItem w => "Weapon",
            ArmorItem a => "Armor",
            _ => "null"
        };
    }
}
