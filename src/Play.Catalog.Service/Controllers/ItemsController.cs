using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Models;
using Play.Catalog.Service.Services;

namespace Play.Catalog.Service.Controllers;


//TODO: Handle exceptions when id is not a valid ObjectId

[Route("api/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{

    IItemsService _itemsService;

    public ItemsController(IItemsService itemsService)
    {
        _itemsService = itemsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemDto>>> Get()
    {
        var items = await _itemsService.GetAsync();
        return Ok(items.Select(item => item.AsDto()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetById(string id)
    {
        var item = await _itemsService.GetAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item.AsDto());
    }

    [HttpPost]
    public async Task<ActionResult<ItemDto>> Post(CreateItemDto createItemDto)
    {
        var item = new Item
        {
            Name = createItemDto.Name,
            Description = createItemDto.Description,
            Price = createItemDto.Price,
            CreatedDate = DateTimeOffset.UtcNow
        };

        await _itemsService.CreateAsync(item);


        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, UpdateItemDto updateItemDto)
    {
        var existingItem = await _itemsService.GetAsync(id);
        if (existingItem == null)
        {
            return NotFound();
        }
        existingItem.Name = updateItemDto.Name;
        existingItem.Description = updateItemDto.Description;
        existingItem.Price = updateItemDto.Price;

        await _itemsService.UpdateAsync(id, existingItem);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var existingItem = await _itemsService.GetAsync(id);
        if (existingItem == null)
        {
            return NotFound();
        }

        await _itemsService.RemoveAsync(existingItem.Id);

        return NoContent();
    }
}

