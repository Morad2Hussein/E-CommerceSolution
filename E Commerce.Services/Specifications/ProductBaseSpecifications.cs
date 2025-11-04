

using E_Commerce.Domain.Entities;

namespace E_Commerce.Services.Specifications
{
    internal class ProductBaseSpecifications : BaseSpecification<Product, int>
    {
        public ProductBaseSpecifications():base() {

            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
