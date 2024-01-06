namespace Play.Catalog.Contracts;

public record CatalogItemCreated(string ItemId, string Name, string Description);

public record CatalogItemUpdated(string ItemId, string Name, string Description);

public record CatalogItemDeleted(string ItemId);