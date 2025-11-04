
using E_Commerce.Domain.Entities.Shared;

namespace E_Commerce.Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity , TKey> specifications);
        Task<TEntity?> GetByIdAsync(TKey id);

        Task AddAsync(TEntity entity);

        void Remove(TEntity entity);
        void Update(TEntity entity);

    }
}
