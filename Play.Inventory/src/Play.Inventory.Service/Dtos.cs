namespace Play.Inventory.Service.Dtos
{
    public record GrantItemsDto(string UserId, string CatalogItemId, int Quantity);

    public record InventoryItemDto(string CatalogItemId, string Name, string Description, int Quantity, DateTimeOffset AcquiredDate);

    public record CatalogItemDto(string Id, string Name, string Description);
}