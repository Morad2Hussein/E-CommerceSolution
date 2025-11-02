
using AutoMapper;
using E_Commerce.Domain.Entities;
using E_Commerce.Shared.DTOS.ProductDTO;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Numerics;

namespace E_Commerce.Services.MappingProfile
{
    public class ProductPictureURLResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _configuration; 
        public ProductPictureURLResolver(IConfiguration configuration) {
         _configuration = configuration; 
        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            // chack if the  PictureUrl not null 
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;
            // This line checks if the PictureUrl property already contains a full URL(i.e., starts with "http" or "https").
            //If it does, it returns it directly — meaning the picture link is already a complete web address.
            if (source.PictureUrl.StartsWith("http"))
                return source.PictureUrl;
            var BaseUrl = _configuration.GetSection("Urls")["BaseUrl"];
            if (string.IsNullOrEmpty(BaseUrl))
                return string.Empty;
            var PicUrl = $"{BaseUrl}/{source.PictureUrl}";
            return PicUrl;
        }
    }
}
