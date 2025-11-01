using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Shared;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Persistence.Repositries
{
    public class GenericRepository<TEntity, TKey> :IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {

        private readonly StoreDbContext _dbContext;
        public GenericRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region 
        //Why we use .Set<TEntity>() instead of a direct DbSet
        //Because this method is generic — it works for any entity type, not just one.
        //Think of DbContext as your database, and Set<TEntity>() as saying:
        //“Get me the correct table for this entity type.”
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbContext.Set<TEntity>().ToListAsync();
        public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);
 
        #endregion

        public async Task<TEntity?> GetByIdAsync(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id);

        public void Remove(TEntity entity)=>  _dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)=> _dbContext.Set<TEntity>().Update(entity);
    }
}
