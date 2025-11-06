using E_Commerce.Domain.Entities.Shared;
using System.Linq.Expressions;

namespace E_Commerce.Domain.Contracts
{
    public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        // Include
        public ICollection<Expression<Func<TEntity, Object>>> IncloudExpression { get; }
        public Expression<Func<TEntity, bool>> Criteria { get; }
        public Expression<Func<TEntity, Object>> OrderBy { get; }
        public Expression<Func<TEntity, Object>> OrderByDescending { get; }
        public int Take { get; }
        public int Skip { get; }
        public bool IsPagination { get; }

    }
}
