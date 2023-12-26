namespace Play.Catalog.Service.Models;

public class CatalogDatabaseSettings
{
    public string ItemsCollectionName { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
}