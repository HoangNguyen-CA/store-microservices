using Play.Catalog.Service.Models;

namespace Play.Catalog.Service.Services;
public interface IRepository<T> where T : IEntity

{
    Task<List<T>> GetAsync();
    Task<T?> GetAsync(string id);
    Task CreateAsync(T entity);
    Task UpdateAsync(string id, T entity);
    Task RemoveAsync(string id);
}