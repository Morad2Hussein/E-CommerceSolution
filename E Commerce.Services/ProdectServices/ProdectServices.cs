
using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.Product_Module;
using E_Commerce.Services_Abstraction.Services;
using E_Commerce.Shared.DTOS.ProductDTO;

namespace E_Commerce.Services.ProdectServices
{
    public class ProdectServices : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProdectServices(IUnitOfWork unitOfWork , IMapper mapper) {
        
         _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #region GetAll Brands
        public async Task<IEnumerable<BrandDTO>> GetAllBrandAsync()
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
           return _mapper.Map<IEnumerable<BrandDTO>>(Brands);
        } 
        #endregion

        public async Task<IEnumerable<ProductDTO>> GetAllProductAsync()
        {
            var Product = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(Product);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
               var Product =await  _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(Product);
        }

        public async Task<IEnumerable<TypeDTO>> GetTypeAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDTO>>(Types);
        }
    }
}
