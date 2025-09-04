using Metin2Api.Domain.Entities;
using Metin2Api.Domain.Repositories;
using Metin2Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Metin2Api.Infrastructure.Repositories
{
    public class CharacterRepository(AppDbContext context) : ICharacterRepository
    {
        private readonly AppDbContext _context = context;
        public async Task AddCharacterAsync(Character character)
        {
            if(character == null)
                throw new ArgumentNullException(nameof(character));

            if (character.Inventory == null)
            {
                character.Inventory = new Inventory
                {
                    Id = character.Id,
                    Items = new List<IItem>()
                };
                _context.Inventories.Add(character.Inventory);
            }

            await _context.Characters.AddAsync(character);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCharacterAsync(int characterId)
        {
            var character = await _context.Characters.FindAsync(characterId);
            
            if(character == null)
                throw new ArgumentNullException(nameof(character));

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Character>> GetAllCharactersAsync()
        {
            var characters = await _context.Characters.ToListAsync();
            
            if(characters == null || characters.Count == 0)
                return new List<Character>();

            return characters;
        }

        public async Task<Character?> GetCharacterByIdAsync(int id)
        {
            var character = await _context.Characters.FindAsync(id);

            if(character == null)
                return null;    

            return character;
        }

        public async Task<Character?> GetCharacterWithBiggestLvl()
        {
            var character = await _context.Characters
                .OrderByDescending(c => c.Level)
                .FirstOrDefaultAsync();

            if(character == null)
                return null;

            return character;
        }

        public async Task<IEnumerable<IItem>> GetItemsByCharacterIdAsync(int characterId)
        {
            var character = await _context.Characters.FindAsync(characterId);

            if(character == null)
                return new List<IItem>();

            var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.CharacterId == characterId);

            if(inventory == null)
                return new List<IItem>();

            return inventory.Items;
        }

        public async Task<IEnumerable<Character>> GetTop10CharactersByLevelAsync()
        {
            var topCharacters = await _context.Characters
                .OrderByDescending(c => c.Level)
                .Take(10)
                .ToListAsync();
            
            if(topCharacters == null || topCharacters.Count == 0)
                return new List<Character>();

            return topCharacters;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
