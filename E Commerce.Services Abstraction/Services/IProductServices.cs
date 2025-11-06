
using E_Commerce.Shared.DTOS.ProductDTO;
using E_Commerce.Shared.ProductQuery;

namespace E_Commerce.Services_Abstraction.Services
{
    public interface IProductServices
    {
        // Get All Products Return IEnumerable Of Products Data Which Will be {Id , Name, Description , PictureUrl , Price , ProductBrand, ProductType} 

        Task<PagiationResult<ProductDTO>> GetAllProductAsync(ProductQueryParams productQuery);
        //Get Product By Id Return Product Data Which Will be {Id , Name, Description , PictureUrl , Price , ProductBrand, ProductType}

        Task<ProductDTO> GetProductByIdAsync(int id);
        //Get All Brands Return IEnumerable Of Brands Data Which Will be {Id , Name}
        Task<IEnumerable<BrandDTO>> GetAllBrandAsync();
        //Get All Types Return IEnumerable Of Types Data Which Will be {Id , Name}
        Task<IEnumerable<TypeDTO>> GetTypeAsync();
       

    }
}
