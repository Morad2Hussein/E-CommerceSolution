
namespace E_Commerce.Shared.DTOS.ProductDTO
{
    public class ProductDTO
    {

        //Get All Products Return IEnumerable Of Products Data Which Will be {Id , Name, Description , PictureUrl , Price , ProductBrand, ProductType} 
     public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public decimal Price { get; set; } 
        public string ProductBrand { get; set; } = default!;
        public string ProductType { get; set; } = default!;


    }
}
