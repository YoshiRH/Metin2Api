using Metin2Api.Domain.Entities;
using Metin2Api.Domain.Repositories;
using Metin2Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Metin2Api.Infrastructure.Repositories
{
    public class ItemRepository(AppDbContext context): IItemRepository
    {
        private readonly AppDbContext _context = context;
        public async Task AddItemAsync(IItem item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));

            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<IItem>> GetAllItemsAsync()
        {
            var items = await _context.Items.ToListAsync();
            
            if(items == null || !items.Any())
                return Enumerable.Empty<IItem>();

            return items;
        }

        public async Task<IItem?> GetItemByIdAsync(int itemId)
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
