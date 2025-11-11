

using E_Commerce.Domain.Entities;
using E_Commerce.Shared.ProductQuery;

namespace E_Commerce.Services.Specifications
{
    internal class ProductCountSpecificaion : BaseSpecification<Product, int>
    {
        public ProductCountSpecificaion(ProductQueryParams productQuery) :
                    base(ProductSpecificationHelper.GetProductCriteria(productQuery))
        {


        }
    }
}
