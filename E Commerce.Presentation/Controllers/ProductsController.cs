using E_Commerce.Services_Abstraction.Services;
using E_Commerce.Shared.DTOS.ProductDTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _services;
        public ProductsController(IProductServices services)
        {
            _services = services;
        }
        #region Get All Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        //This is the return type of the method.
        {

            var Products = await _services.GetAllProductAsync();
            return Ok(Products);
        }

        #endregion
        #region Get Product By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var Product =await _services.GetProductByIdAsync(id);
            return Ok(Product);
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
