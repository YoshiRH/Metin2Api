using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Metin2Api.Application.Dtos;
using Metin2Api.Application.Services;

namespace Metin2Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class GuildController(IGuildService guildService) : ControllerBase
    {
        private readonly IGuildService _guildService = guildService;

        [HttpPost("Guilds/CreateGuild")]
        public async Task<ActionResult> CreateGuild(CreateGuildDto guildDto)
        {
            var result = await _guildService.CreateGuildAsync(guildDto);
            return Ok(result);
        }

        [HttpGet("Guilds")]
        public async Task<ActionResult> GetAllGuilds()
        {
            var guilds = await _guildService.GetAllGuildsAsync();
            return Ok(guilds);
        }

        [HttpGet("Guilds/{id}")]
        public async Task<ActionResult> GetGuildById(int id)
        {
            var guild = await _guildService.GetGuildByIdAsync(id);
            return Ok(guild);
        }

        [HttpGet("Guilds/ByName/{name}")]
        public async Task<ActionResult> GetGuildByName(string name)
        {
            var guild = await _guildService.GetGuildByNameAsync(name);
            return Ok(guild);
        }

        [HttpPost("Guilds/{guildId}/AddCharacter/{characterId}")]
        public async Task<ActionResult> AddCharacterToGuild(int guildId, int characterId)
        {
            var result = await _guildService.AddCharacterToGuildAsync(guildId, characterId);
            return Ok(result);
        }

        [HttpPost("Guilds/{guildId}/RemoveCharacter/{characterId}")]
        public async Task<ActionResult> RemoveCharacterFromGuild(int guildId, int characterId)
        {
            var result = await _guildService.RemoveCharacterFromGuildAsync(guildId, characterId);
            return Ok(result);
        }

        [HttpDelete("Guilds/{id}")]
        public async Task<ActionResult> DeleteGuildById(int id)
        {
            var result = await _guildService.DeleteGuildAsync(id);
            return result ? Ok(result) : BadRequest();
        }
    }
}
