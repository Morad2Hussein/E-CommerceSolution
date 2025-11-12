
using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Basket_Module;
using E_Commerce.Services_Abstraction.Services;
using E_Commerce.Shared.DTOS.BasketDTO;

namespace E_Commerce.Services.BasketServices
{
    public class BasketServices : IBasketServices
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        public BasketServices(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basket)
        {
            var CustomerBasket = _mapper.Map<CustomerBasket>(basket);
            var CreateCustomerBasket = await _basketRepository.CreateOrUpdateBasketAsync(CustomerBasket);
            return _mapper.Map<CustomerBasket, BasketDTO>(CreateCustomerBasket!);
        }

        public Task<bool> DeleteBasketAsync(string Id)=> _basketRepository.DeleteBasketAsync(Id);   

        public async Task<BasketDTO> GetBasketAysnc(string Id)
        {
            var Basket =await  _basketRepository.GetBasketAsync(Id);
            return _mapper.Map<CustomerBasket, BasketDTO>(Basket!);
        }
    }
}
