using E_Commerce.Domain.Entities.Product_Module;
using E_Commerce.Domain.Entities.Shared;

namespace E_Commerce.Domain.Entities
{
    public class Product : BaseEntity<int>
    {
        #region Properties
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public decimal Price { get; set; }

        #endregion

        #region RelationShip
        public int BrandId { get; set; } 
        public ProductBrand ProductBrand { get; set; } = default!;

        public int TypeId { get; set; }
        public ProductType ProductType { get; set; } = default!;
        #endregion



    }
}

