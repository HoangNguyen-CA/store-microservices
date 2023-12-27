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

    IRepository<Item> _itemsRepository;

    public ItemsController(IRepository<Item> itemsRepository)
    {
        _itemsRepository = itemsRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemDto>>> Get()
    {
        var items = await _itemsRepository.GetAsync();
        return Ok(items.Select(item => item.AsDto()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetById(string id)
    {
        var item = await _itemsRepository.GetAsync(id);
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

        await _itemsRepository.CreateAsync(item);


        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, UpdateItemDto updateItemDto)
    {
        var existingItem = await _itemsRepository.GetAsync(id);
        if (existingItem == null)
        {
            return NotFound();
        }
        existingItem.Name = updateItemDto.Name;
        existingItem.Description = updateItemDto.Description;
        existingItem.Price = updateItemDto.Price;

        await _itemsRepository.UpdateAsync(id, existingItem);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var existingItem = await _itemsRepository.GetAsync(id);
        if (existingItem == null)
        {
            return NotFound();
        }

        await _itemsRepository.RemoveAsync(existingItem.Id);

        return NoContent();
    }
}

