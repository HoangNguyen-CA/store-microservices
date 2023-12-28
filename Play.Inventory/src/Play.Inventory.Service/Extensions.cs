using Play.Inventory.Service.Models;
using Play.Inventory.Service.Dtos;

namespace Play.Inventory.Service;


public static class Extensions
{
    public static InventoryItemDto AsDto(this InventoryItem item)
    {
        return new InventoryItemDto(item.CatalogItemId, item.Quantity, item.AcquiredDate);
    }
}