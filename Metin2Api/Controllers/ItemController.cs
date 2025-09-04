using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Metin2Api.Application.Dtos;
using Metin2Api.Application.Services;

namespace Metin2Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController(IItemService itemService) : ControllerBase
    {
        private readonly IItemService _itemService = itemService;

        [HttpPost("Characters/{id}/Items")]
        public async Task<ActionResult> AddItem(int characterId, CreateItemDto itemDto)
        {
            var result = await _itemService.AddItemToCharacterAsync(itemDto, characterId);
            return Ok(result);
        }

        [HttpGet("Items")]
        public async Task<ActionResult> GetAllItems()
        {
            var items = await _itemService.GetAllItemsAsync();
            return Ok(items);
        }

        [HttpGet("Items/{id}")]
        public async Task<ActionResult> GetItemById(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            return Ok(item);
        }
    }
}
