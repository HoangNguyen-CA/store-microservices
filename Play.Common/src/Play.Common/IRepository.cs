
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Play.Common
{
    public interface IRepository<T> where T : IEntity
    {
        Task<List<T>> GetAllAsync();


        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter);

        Task<T?> GetAsync(string id);

        Task<T?> GetAsync(Expression<Func<T, bool>> filter);

        Task CreateAsync(T entity);
        Task UpdateAsync(string id, T entity);
        Task RemoveAsync(string id);
    }
}