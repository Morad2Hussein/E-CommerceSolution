

using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Shared;
using System.Linq.Expressions;

namespace E_Commerce.Services.Specifications
{
    internal class BaseSpecification<TEntiy, TKey> : ISpecifications<TEntiy, TKey> where TEntiy : BaseEntity<TKey>
    {
        public ICollection<Expression<Func<TEntiy, object>>> IncloudExpression { get; } = [];
        protected void AddInclude(Expression<Func<TEntiy,Object>> IncludeExpression)
        {
           IncloudExpression.Add(IncludeExpression); 
        }

    }
}
