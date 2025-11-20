using E_Commerce.Presentation.Attributes;
using E_Commerce.Services_Abstraction.Services;
using E_Commerce.Shared.DTOS.ProductDTO;
using E_Commerce.Shared.ProductQuery;
using Microsoft.AspNetCore.Mvc;
namespace E_Commerce.Presentation.Controllers
{
    
    public class ProductsController : ApiBaseController
    {
        private readonly IProductServices _services;
        public ProductsController(IProductServices services)
        {
            _services = services;
        }
        #region Get All Product
        [HttpGet]
        [RedisCache]
        public async Task<ActionResult<PagiationResult<IEnumerable<ProductDTO>>>> GetAllProducts([FromQuery]ProductQueryParams productQuery)
        //This is the return type of the method.
        {

            var Products = await _services.GetAllProductAsync(productQuery);
            return Ok(Products);
        }

        #endregion
        #region Get Product By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var Results =await _services.GetProductByIdAsync(id);

            return HandleResult<ProductDTO> (Results);
        }


        #endregion
        #region Get All Product Types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllProductTypes()
        {
            var ProductTypes = await _services.GetTypeAsync();
            return Ok(ProductTypes);
        }
        #endregion
        #region Get All Product Brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllProductBrands()
        { 
            var ProductBrands =await _services.GetAllBrandAsync();
            return Ok(ProductBrands);
        }
        #endregion
    }
}
