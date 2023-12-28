namespace Play.Inventory.Service.Dtos
{
    public record GrantItemsDto(string UserId, string CatalogItemId, int Quantity);

    public record InventoryItemDto(string CatalogItemId, int Quantity, DateTimeOffset AcquiredDate);


}