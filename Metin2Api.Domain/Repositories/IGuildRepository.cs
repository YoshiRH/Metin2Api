using Metin2Api.Domain.Entities;

namespace Metin2Api.Domain.Repositories
{
    public interface IGuildRepository
    {
        Task<Guild?> GetByIdAsync(int id);
        Task<Guild?> GetByNameAsync(string name);
        Task<List<Guild>> GetAllAsync();
        Task AddAsync(Guild guild, Character master);
        Task DeleteAsync(Guild guild);
        Task AddCharacterToGuildAsync (int guildId, Character character);
        Task RemoveCharacterFromGuildAsync (int guildId, Character character);
        Task SaveChangesAsync();
    }
}
