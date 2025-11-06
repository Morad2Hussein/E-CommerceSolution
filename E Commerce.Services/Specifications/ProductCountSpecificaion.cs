

using E_Commerce.Domain.Entities;
using E_Commerce.Shared.ProductQuery;

namespace E_Commerce.Services.Specifications
{
    internal class ProductCountSpecificaion : BaseSpecification<Product , int> 
    {
        public ProductCountSpecificaion(ProductQueryParams productQuery) : base(
                 p =>
                                      (!productQuery.BrandId.HasValue || p.BrandId == productQuery.BrandId.Value) &&
                                      (!productQuery.TypeId.HasValue || p.TypeId == productQuery.TypeId.Value) &&
                                      (string.IsNullOrEmpty(productQuery.Search) ||
                                       p.Name.Contains(productQuery.Search, StringComparison.OrdinalIgnoreCase))
                                )
        {
        
        
        }
    }
}
