using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClienteApp.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity, TKey> : IDisposable where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity,bool>> predicate);
        Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> GetByIdAsync(TKey id);
    }
}
