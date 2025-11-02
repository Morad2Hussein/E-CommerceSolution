
using AutoMapper;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.Product_Module;
using E_Commerce.Shared.DTOS.ProductDTO;

namespace E_Commerce.Services.MappingProfile
{
    public class ProductProfile : Profile
    {
        public ProductProfile() {

            CreateMap<ProductBrand, BrandDTO>();
            CreateMap<Product, ProductDTO>()
                   .ForMember(dest => dest.ProductType, opt => opt.MapFrom(scr => scr.ProductType.Name))
                   .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(scr => scr.ProductBrand.Name))
                    .ForMember(dest=>dest.PictureUrl , opt => opt.MapFrom<ProductPictureURLResolver>());
            
            CreateMap<ProductType, TypeDTO>();
        }
    }
}
