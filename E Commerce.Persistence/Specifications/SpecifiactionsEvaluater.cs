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
                if (specifications.IncloudExpression is not null && specifications.IncloudExpression.Any())
                {
                    Query = specifications.IncloudExpression.Aggregate( Query , (CurrentQuery, IncludeExp) => CurrentQuery.Include(navigationPropertyPath: IncludeExp));
                }
            }

            return Query;

        }

    }
}
