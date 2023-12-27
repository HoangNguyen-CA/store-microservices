namespace Play.Catalog.Service.Models;

public class CatalogDatabaseSettings
{
    public string ItemsCollectionName { get; set; } = null!;
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
}