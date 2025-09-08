using Metin2Api.Application.Dtos;
using Metin2Api.Domain.Entities;
using Metin2Api.Domain.Repositories;

namespace Metin2Api.Application.Services
{
    public class GuildService(IGuildRepository guildRepository, ICharacterRepository characterRepository) : IGuildService
    {
        private readonly IGuildRepository _guildRepository = guildRepository;
        private readonly ICharacterRepository _characterRepository = characterRepository;

        public async Task<string> AddCharacterToGuildAsync(int guildId, int characterId)
        {
            var guild = await _guildRepository.GetByIdAsync(guildId);

            if (guild == null)
            {
                return "Guild not found.";
            }

            var character = await _characterRepository.GetCharacterByIdAsync(characterId);

            if (character == null)
            {
                return "Character not found.";
            }

            if (character.GuildId != null)
            {
                return "Character is already in a guild.";
            }

            await _guildRepository.AddCharacterToGuildAsync(guildId, character);
            await _guildRepository.SaveChangesAsync();

            character.GuildId = guildId;
            await _characterRepository.SaveChangesAsync();
            
            return "Character added to guild successfully.";
        }

        public async Task<string> CreateGuildAsync(CreateGuildDto guildDto)
        {
            if(guildDto == null)
                throw new ArgumentNullException(nameof(guildDto));

            if(string.IsNullOrWhiteSpace(guildDto.Name))
                return "Guild name cannot be empty.";

            if(await _guildRepository.GetByNameAsync(guildDto.Name) != null)
                return "Guild name already exists.";

            var master = await _characterRepository.GetCharacterByIdAsync(guildDto.MasterId);

            if(master == null)
                return "Master character not found.";

            if(master.GuildId != null)
                return "Master character is already in a guild.";

            var newGuild = new Guild
            {
                GuildName = guildDto.Name,
                MasterId = guildDto.MasterId,
                Characters = new List<Character>()
            };

            await _guildRepository.AddAsync(newGuild, master);
            await _guildRepository.SaveChangesAsync();

            return "Guild created successfully.";
        }

        public async Task<bool> DeleteGuildAsync(int guildId)
        {
            var guild = await _guildRepository.GetByIdAsync(guildId);

            if(guild == null)
                return false;

            await _guildRepository.DeleteAsync(guild);
            await _guildRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<GuildDto>> GetAllGuildsAsync()
        {
            var guilds = await _guildRepository.GetAllAsync();

            return guilds.Select(g => new GuildDto
            {
                Id = g.Id,
                Name = g.GuildName,
                MasterId = g.MasterId,
                MemberIds = g.Characters?.Select(c => c.Id).ToList() ?? new List<int>()
            });
        }

        public async Task<GuildDto?> GetGuildByIdAsync(int guildId)
        {
            var guild = await _guildRepository.GetByIdAsync(guildId);

            if(guild == null)
                return null;

            return new GuildDto
            {
                Id = guild.Id,
                Name = guild.GuildName,
                MasterId = guild.MasterId,
                MemberIds = guild.Characters?.Select(c => c.Id).ToList() ?? new List<int>()
            };
        }

        public async Task<GuildDto?> GetGuildByNameAsync(string name)
        {
            var guild = await _guildRepository.GetByNameAsync(name);

            if (guild == null)
                return null;

            return new GuildDto
            {
                Id = guild.Id,
                Name = guild.GuildName,
                MasterId = guild.MasterId,
                MemberIds = guild.Characters?.Select(c => c.Id).ToList() ?? new List<int>()
            };
        }

        public async Task<string> RemoveCharacterFromGuildAsync(int guildId, int characterId)
        {
            var guild = await _guildRepository.GetByIdAsync(guildId);

            if (guild == null)
                return "Guild not found.";

            var character = await _characterRepository.GetCharacterByIdAsync(characterId);

            if (character == null)
                return "Character not found.";

            if (character.GuildId != guildId)
                return "Character is not in the specified guild.";

            await _guildRepository.RemoveCharacterFromGuildAsync(guildId, character);
            await _guildRepository.SaveChangesAsync();

            return "Character removed from guild successfully.";
        }
    }
}
