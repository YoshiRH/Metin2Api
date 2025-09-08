using Metin2Api.Domain.Entities;
using Metin2Api.Domain.Repositories;
using Metin2Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Metin2Api.Infrastructure.Repositories
{
    public class ItemRepository(AppDbContext context): IItemRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Item?> AddItemToCharacterAsync(int charactedId, Item item)
        {
            var character = await _context.Characters.FindAsync(charactedId);

            if (character == null)
                return null;

            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            var items = await _context.Items.ToListAsync();
            
            if(items == null || !items.Any())
                return Enumerable.Empty<Item>();

            return items;
        }

        public async Task<Item?> GetItemByIdAsync(int itemId)
        {
            var item = await _context.Items.FindAsync(itemId);

            if(item is null)
                return null;

            return item;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
