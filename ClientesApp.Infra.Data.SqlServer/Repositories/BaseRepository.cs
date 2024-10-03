using ClienteApp.Domain.Interfaces.Repositories;
using ClientesApp.Infra.Data.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Infra.Data.SqlServer.Repositories
{
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        private readonly DataContext _dataContext;

        public BaseRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dataContext.AddAsync(entity);
            await _dataContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _dataContext.Update(entity);
            await _dataContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            _dataContext.Remove(entity);  
            await _dataContext.SaveChangesAsync();
        }

        public virtual void Dispose()
        {
            _dataContext.Dispose();
        }

        public virtual async Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dataContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _dataContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dataContext.Set<TEntity>().Where(predicate).FirstOrDefaultAsync();
        }

        public virtual async Task<bool> VerifyExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dataContext.Set<TEntity>().AnyAsync(predicate);
        }

    }
}
