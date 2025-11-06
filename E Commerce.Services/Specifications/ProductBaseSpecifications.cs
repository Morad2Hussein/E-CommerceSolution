using E_Commerce.Domain.Entities;
using E_Commerce.Shared.ProductQuery;

namespace E_Commerce.Services.Specifications
{
    internal class ProductBaseSpecifications : BaseSpecification<Product, int>
    {
        #region using id - Criteria
        public ProductBaseSpecifications(int id) : base(p => p.Id == id)
        {

            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
        #endregion
        #region get all data 
        public ProductBaseSpecifications(ProductQueryParams productQuery) :
                            base(
                 p =>
                                      (!productQuery.BrandId.HasValue || p.BrandId == productQuery.BrandId.Value) &&
                                      (!productQuery.TypeId.HasValue || p.TypeId == productQuery.TypeId.Value) &&
                                      (string.IsNullOrEmpty(productQuery.Search) ||
                                       p.Name.Contains(productQuery.Search, StringComparison.OrdinalIgnoreCase))
                                )

        {

            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            switch (productQuery.sort)
            {
                case ProductSortingOptions.NameAcs :
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDecs :
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAcs :
                    AddOrderBy(p => p.Price);
                    break;
                    case ProductSortingOptions.PriceDecs :
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Id);
                    break;

                         
            }
            ApplyPaginations(productQuery.PageSize, productQuery.PageIndex);
        }

        #endregion
    }
}
