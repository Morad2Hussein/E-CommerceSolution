using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Specifications
{
    internal static class SpecifiactionsEvaluater
    {
        // create qurey
        public static IQueryable<TEntity> CreateQurey<TEntity, TKey>
            (IQueryable<TEntity> StartPoint,
                ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var Query = StartPoint;
            if (specifications is not null)
            {
                if (specifications.Criteria is not null )
                {
                    Query = Query.Where(specifications.Criteria);  
                }
                if (specifications.IncloudExpression is not null && specifications.IncloudExpression.Any())
                {
                    Query = specifications.IncloudExpression.Aggregate( Query , (CurrentQuery, IncludeExp) => CurrentQuery.Include(navigationPropertyPath: IncludeExp));
                }
                if (specifications.OrderBy is not null )
                {
                    Query = Query.OrderBy(specifications.OrderBy);
                }
                if (specifications.OrderByDescending is not null )
                {
                    Query = Query.OrderBy(specifications.OrderByDescending);
                }
                if (specifications.IsPagination)
                {
                    Query = Query.Skip(specifications.Skip).Take(specifications.Take);
                }
            }

            return Query;

        }

    }
}
