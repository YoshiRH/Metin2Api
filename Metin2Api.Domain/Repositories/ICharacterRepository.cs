using Metin2Api.Domain.Entities;

namespace Metin2Api.Domain.Repositories
{
    public interface ICharacterRepository
    {
        Task<IEnumerable<Character>> GetAllCharactersAsync();
        Task<IEnumerable<IItem>> GetItemsByCharacterIdAsync(int characterId);
        Task<Character?> GetCharacterByIdAsync(int id);
        Task<Character?> GetCharacterWithBiggestLvl();
        Task<IEnumerable<Character>> GetTop10CharactersByLevelAsync();
        Task AddCharacterAsync(Character character);
        Task DeleteCharacterAsync(int characterId);
        Task SaveChangesAsync();

    }
}
