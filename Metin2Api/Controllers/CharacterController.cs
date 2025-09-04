using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Metin2Api.Application.Dtos;
using Metin2Api.Application.Services;

namespace Metin2Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController(ICharacterService characterService) : ControllerBase
    {
        private readonly ICharacterService _characterService = characterService;

        [HttpPost("Accounts/{id}/CreateCharacter")]
        public async Task<ActionResult> AddCharacter(int accountId, CreateCharacterDto characterDto)
        {
            var result = await _characterService.AddCharacterToAccountAsync(characterDto, accountId);
            return Ok(result);
        }

        [HttpGet("Characters")]
        public async Task<ActionResult> GetAllCharacters()
        {
            var characters = await _characterService.GetAllCharactersAsync();
            return Ok(characters);
        }

        [HttpGet("Accounts/{id}")]
        public async Task<ActionResult> GetCharacterByAccount(int id)
        {
            var character = await _characterService.GetCharacterByIdAsync(id);
            return Ok(character);
        }

        [HttpGet("Ranking/top1")]
        public async Task<ActionResult> GetTop1Character()
        {
            var character = await _characterService.GetCharacterWithBiggestLvl();
            return Ok(character);
        }

        [HttpGet("Ranking/top10")]
        public async Task<ActionResult> GetTop10Characters()
        {
            var characters = await _characterService.GetTop10CharactersByLevelAsync();
            return Ok(characters);
        }

        [HttpGet("Characters/{id}/Items")]
        public async Task<ActionResult> GetCharacterItems(int id)
        {
            var items = await _characterService.GetItemsByCharacterIdAsync(id);
            return Ok(items);
        }

        [HttpDelete("Characters/{id}")]
        public async Task<ActionResult> DeleteCharacterById(int id)
        {
            var result = await _characterService.DeleteCharacterAsync(id);
            return result ? Ok(result) : BadRequest();
        }

    }
}
