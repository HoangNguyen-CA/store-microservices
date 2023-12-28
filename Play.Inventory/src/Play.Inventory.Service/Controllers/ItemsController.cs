using Microsoft.AspNetCore.Mvc;
using Play.Inventory.Service.Dtos;
using Play.Inventory.Service.Models;
using Play.Common;


namespace Play.Inventory.Service.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{

    private readonly IRepository<InventoryItem> _itemsRepository;

    public ItemsController(IRepository<InventoryItem> itemsRepository)
    {
        _itemsRepository = itemsRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAsync(string userId)
    {
        if (userId == "")
        {
            return BadRequest();
        }

        var items = (await _itemsRepository.GetAllAsync(item => item.UserId == userId))
            .Select(item => item.AsDto());

        return Ok(items);
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync(GrantItemsDto grantItemsDto)
    {
        var inventoryItem = await _itemsRepository.GetAsync(
            item => item.UserId == grantItemsDto.UserId && item.CatalogItemId == grantItemsDto.CatalogItemId);

        if (inventoryItem == null)
        {
            inventoryItem = new InventoryItem
            {
                CatalogItemId = grantItemsDto.CatalogItemId,
                UserId = grantItemsDto.UserId,
                Quantity = grantItemsDto.Quantity,
                AcquiredDate = DateTimeOffset.UtcNow
            };

            await _itemsRepository.CreateAsync(inventoryItem);
        }
        else
        {
            inventoryItem.Quantity += grantItemsDto.Quantity;
            await _itemsRepository.UpdateAsync(inventoryItem.Id, inventoryItem);
        }

        return Ok();
    }

}