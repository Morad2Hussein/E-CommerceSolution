using AutoMapper;
using E_Commerce.Domain.Entities.Basket_Module;
using E_Commerce.Shared.DTOS.BasketDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.MappingProfile
{
    public class BasketMapping : Profile
    {
        public BasketMapping()
        {
            CreateMap<CustomerBasket, BasketDTO>().ReverseMap();
            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();
        }
    }
}
