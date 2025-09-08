using Metin2Api.Domain.Entities;
using Metin2Api.Domain.Repositories;
using Metin2Api.Application.Dtos;

namespace Metin2Api.Application.Services
{
    public interface IGuildService
    {
        Task<string> CreateGuildAsync(CreateGuildDto guildDto);
        Task<bool> DeleteGuildAsync(int guildId);
        Task<IEnumerable<GuildDto>> GetAllGuildsAsync();
        Task<GuildDto?> GetGuildByIdAsync(int guildId);
        Task<GuildDto?> GetGuildByNameAsync(string name);
        Task<string> AddCharacterToGuildAsync(int guildId, int characterId);
        Task<string> RemoveCharacterFromGuildAsync(int guildId, int characterId);
    }
}
