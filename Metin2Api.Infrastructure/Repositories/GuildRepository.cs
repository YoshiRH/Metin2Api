using Metin2Api.Domain.Entities;
using Metin2Api.Domain.Repositories;
using Metin2Api.Infrastructure.Data;   
using Microsoft.EntityFrameworkCore;

namespace Metin2Api.Infrastructure.Repositories
{
    public class GuildRepository(AppDbContext context) : IGuildRepository
    {
        private readonly AppDbContext _context = context;
        public async Task AddAsync(Guild guild, Character master)
        {
            if(guild == null)
                throw new ArgumentNullException(nameof(guild)); 

            if(master == null)
                throw new ArgumentNullException(nameof(master));

            guild.MasterId = master.Id;

            await _context.Guilds.AddAsync(guild);
            guild.Characters.Add(master);


            await _context.SaveChangesAsync();
        }

        public async Task AddCharacterToGuildAsync(int guildId, Character character)
        {
            if(character == null)
                throw new ArgumentNullException(nameof(character));

            var guild = await _context.Guilds.Include(g => g.Characters).FirstOrDefaultAsync(g => g.Id == guildId);

            if(guild == null)
                throw new InvalidOperationException($"Guild with ID {guildId} not found.");

            guild.Characters.Add(character);
            character.GuildId = guild.Id;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guild guild)
        {
            if(guild == null)
                throw new ArgumentNullException(nameof(guild));

            _context.Guilds.Remove(guild);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Guild>> GetAllAsync()
        {
            var guilds = await _context.Guilds.ToListAsync();

            if(guilds == null || !guilds.Any())
                return new List<Guild>();

            return guilds;
        }

        public async Task<Guild?> GetByIdAsync(int id)
        {
            var guild = await _context.Guilds.Include(g => g.Characters).FirstOrDefaultAsync(g => g.Id == id);

            if(guild == null)
                return null;

            return guild;
        }

        public async Task<Guild?> GetByNameAsync(string name)
        {
            var guild = await _context.Guilds.Include(g => g.Characters).FirstOrDefaultAsync(g => g.GuildName == name);

            if (guild == null)
                return null;

            return guild;
        }

        public async Task RemoveCharacterFromGuildAsync(int guildId, Character character)
        {
            var guild = await _context.Guilds.Include(g => g.Characters).FirstOrDefaultAsync(g => g.Id == guildId);

            if (guild == null)
                throw new InvalidOperationException($"Guild with ID {guildId} not found.");

            if (character == null)
                throw new ArgumentNullException(nameof(character));

            if (!guild.Characters.Remove(character))
                throw new InvalidOperationException($"Character with ID {character.Id} is not in guild with ID {guildId}.");

            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
