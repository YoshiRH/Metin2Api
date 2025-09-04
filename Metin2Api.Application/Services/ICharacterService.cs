using Metin2Api.Domain.Entities;
using Metin2Api.Application.Dtos;

namespace Metin2Api.Application.Services
{
    public interface ICharacterService
    {
        Task<IEnumerable<CharacterDto>> GetAllCharactersAsync();
        Task<IEnumerable<ItemDto>> GetItemsByCharacterIdAsync(int characterId);
        Task<CharacterDto?> GetCharacterByIdAsync(int id);
        Task<CharacterDto?> GetCharacterWithBiggestLvl();
        Task<IEnumerable<CharacterDto>> GetTop10CharactersByLevelAsync();
        Task<string> AddCharacterToAccountAsync(CreateCharacterDto character, int accountId);
        Task DeleteCharacterAsync(CharacterDto character);
    }
}
