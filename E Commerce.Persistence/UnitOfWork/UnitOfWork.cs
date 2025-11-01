using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Shared;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Persistence.Repositries;
using System.Collections.Generic;

namespace E_Commerce.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = [];
        private readonly StoreDbContext _dbContext;
        public UnitOfWork(StoreDbContext dbContext)
        {

            _dbContext = dbContext;

        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>(TEntity entity) where TEntity : BaseEntity<TKey>
        {
            // create object from GenericRepository
            // select the type of entity
            var EntityType = typeof(TEntity);
            // check if the type avaible or not 
            if (_repositories.TryGetValue(EntityType, out object? repositries))
                return (IGenericRepository<TEntity, TKey>)repositries;
            //Here, the code checks if there’s already a repository created for this entity type.
            //TryGetValue:
            //Returns true if the entity type exists in the _repositories dictionary.
            //If yes → returns that repository after casting it back to the correct type.
            //This avoids creating duplicate repositories for the same entity.
            var newRepo = new GenericRepository<TEntity, TKey>(_dbContext);
            _repositories[EntityType] = newRepo;
            return newRepo;
            // Stores the new repository instance in the dictionary.
            //So, if you call GetRepository for the same entity again, it will reuse this one.
        }






        public async Task<int> SaveChangeAsync() => await _dbContext.SaveChangesAsync();
    }
}
