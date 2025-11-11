using E_Commerce.Domain.Entities;
using E_Commerce.Shared.ProductQuery;
using System.Linq.Expressions;

namespace E_Commerce.Services.Specifications
{
    internal static class ProductSpecificationHelper
    {
        public static Expression<Func<Product, bool>> GetProductCriteria(ProductQueryParams productQuery)
        { 
            return p =>
                (!productQuery.BrandId.HasValue || p.BrandId == productQuery.BrandId.Value) &&
                (!productQuery.TypeId.HasValue || p.TypeId == productQuery.TypeId.Value) &&
                (string.IsNullOrEmpty(productQuery.Search) ||
                 p.Name.Contains(productQuery.Search, StringComparison.OrdinalIgnoreCase));

        }

    }

}
