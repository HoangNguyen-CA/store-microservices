using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Play.Catalog.Service.Models;

namespace Play.Catalog.Service.Services;

public class ItemsService : IItemsService
{
    private readonly IMongoCollection<Item> _itemsCollection;

    public ItemsService(IOptions<CatalogDatabaseSettings> databaseSettings, IMongoClient client)
    {
        var database = client.GetDatabase(databaseSettings.Value.DatabaseName);
        _itemsCollection = database.GetCollection<Item>(databaseSettings.Value.ItemsCollectionName);
    }

    public async Task<List<Item>> GetAsync()
    {
        return await _itemsCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Item?> GetAsync(string id)
    {
        return await _itemsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
    public async Task CreateAsync(Item item)
    {
        await _itemsCollection.InsertOneAsync(item);
    }

    public async Task UpdateAsync(string id, Item item)
    {
        await _itemsCollection.ReplaceOneAsync(x => x.Id == id, item);
    }

    public async Task RemoveAsync(string id)
    {
        await _itemsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
