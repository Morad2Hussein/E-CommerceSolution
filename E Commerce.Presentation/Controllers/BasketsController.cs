using E_Commerce.Services_Abstraction.Services;
using E_Commerce.Shared.DTOS.BasketDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketServices _basketServices;
        public BasketsController(IBasketServices basketServices)
        {
            _basketServices = basketServices;
        }
        #region Get Basket
        [HttpGet]
        public async Task<ActionResult<BasketDTO>> GetBasket(string id)
        {
            var Basket = await _basketServices.GetBasketAysnc(id);
            return Ok(Basket);
        }
        #endregion
        #region Delete Basket
        [HttpDelete("{id}")]
        public async Task<ActionResult<BasketDTO>> DeleteBasket(string id)
        {
            var Result = await _basketServices.DeleteBasketAsync(id);
            return Ok(Result);

        }

        #endregion

        #region Create or Update Basket
        [HttpPost]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdateBasket( BasketDTO basket)
        {
            var Basket = await _basketServices.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }
        #endregion
    }
}
