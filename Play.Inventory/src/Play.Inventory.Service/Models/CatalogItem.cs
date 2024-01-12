using Play.Common;

namespace Play.Inventory.Service.Models;

public class CatalogItem : IEntity
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;
}