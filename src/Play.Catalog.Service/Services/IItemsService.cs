using Play.Catalog.Service.Models;

namespace Play.Catalog.Service.Services;
public interface IItemsService
{
    Task<List<Item>> GetAsync();
    Task<Item?> GetAsync(string id);
    Task CreateAsync(Item item);
    Task UpdateAsync(string id, Item item);
    Task RemoveAsync(string id);
}