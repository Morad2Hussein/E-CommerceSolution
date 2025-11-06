

using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Shared;
using System.Linq.Expressions;

namespace E_Commerce.Services.Specifications
{
    internal class BaseSpecification<TEntiy, TKey> : ISpecifications<TEntiy, TKey> where TEntiy : BaseEntity<TKey>
    {
        #region Criteria

        // A property that holds a filtering condition (where clause) as an expression.
        public Expression<Func<TEntiy, bool>> Criteria { get; }
        // Constructor that initializes the specification with a given condition.
        protected BaseSpecification(Expression<Func<TEntiy, bool>> criteriaExpression)
        {
            // Assign the passed expression (criteriaExpression) to the Criteria property.
            // This allows derived specifications to define custom filtering logic.
            Criteria = criteriaExpression;
        }
        #endregion
        #region Includes
        // A collection of "include" expressions used to specify related entities 
        // that should be eagerly loaded when querying the database.
        public ICollection<Expression<Func<TEntiy, object>>> IncloudExpression { get; } = [];
        // Protected method to add a new include expression to the collection.
        protected void AddInclude(Expression<Func<TEntiy, Object>> IncludeExpression)
        {
            IncloudExpression.Add(IncludeExpression);
        }
        #endregion
        #region Sorting
        // Expression that defines the property to sort by in ascending order.
        // Example: product => product.Name
        public Expression<Func<TEntiy, object>> OrderBy { get; private set; }
        // Expression that defines the property to sort by in descending order.
        // Example: product => product.Price
        public Expression<Func<TEntiy, object>> OrderByDescending { get; private set; }


        #region order functions 
        protected void AddOrderBy (Expression<Func<TEntiy,Object>> OderByExpression)
        {
            OrderBy = OderByExpression;
        }
        protected void AddOrderByDescending (Expression<Func<TEntiy,Object>> OderByDesExpression)
        {
            OrderBy = OderByDesExpression;
        }
        #endregion

        #endregion
        #region Pagination
        public int Take { get; private set ; }
        public int Skip { get; private set;  }
        public bool IsPagination { get; private set; }
        protected void ApplyPaginations (int pageSize , int pageIndex)
        {
            IsPagination = true;
            Take = pageSize;
            Skip = (pageIndex - 1) *pageSize;

        }
        #endregion 

    }
}
