using Play.Common;

namespace Play.Inventory.Service.Models;

public class InventoryItem : IEntity
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string CatalogItemId { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTimeOffset AcquiredDate { get; set; }


}