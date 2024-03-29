using Microsoft.AspNetCore.Mvc;
using Play.Inventory.Service.Dtos;
using Play.Inventory.Service.Models;
using Play.Inventory.Service.Clients;
using Play.Common;



namespace Play.Inventory.Service.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{

    private readonly IRepository<InventoryItem> _inventoryItemsRepository;

    private readonly IRepository<CatalogItem> _catalogItemsRepository;

    public ItemsController(IRepository<InventoryItem> inventoryItemsRepository, IRepository<CatalogItem> catalogItemsRepository)
    {
        _inventoryItemsRepository = inventoryItemsRepository;
        _catalogItemsRepository = catalogItemsRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAsync(string userId)
    {
        if (userId == "")
        {
            return BadRequest();
        }


        var inventoryItemEntities = await _inventoryItemsRepository.GetAllAsync(item => item.UserId == userId);

        var itemIds = inventoryItemEntities.Select(item => item.CatalogItemId);

        var catalogItems = await _catalogItemsRepository.GetAllAsync(item => itemIds.Contains(item.Id));

        var inventoryItemDtos = inventoryItemEntities.Select(inventoryItem =>
        {
            var catalogItem = catalogItems.Single(catalogItem => catalogItem.Id == inventoryItem.CatalogItemId);

            return inventoryItem.AsDto(catalogItem.Name, catalogItem.Description);
        });

        return Ok(inventoryItemDtos);
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync(GrantItemsDto grantItemsDto)
    {
        var inventoryItem = await _inventoryItemsRepository.GetAsync(
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

            await _inventoryItemsRepository.CreateAsync(inventoryItem);
        }
        else
        {
            inventoryItem.Quantity += grantItemsDto.Quantity;
            await _inventoryItemsRepository.UpdateAsync(inventoryItem.Id, inventoryItem);
        }

        return Ok();
    }

}