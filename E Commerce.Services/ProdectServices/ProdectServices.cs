
using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.Product_Module;
using E_Commerce.Services.Specifications;
using E_Commerce.Services_Abstraction.Services;
using E_Commerce.Shared.DTOS.ProductDTO;
using E_Commerce.Shared.ProductQuery;

namespace E_Commerce.Services.ProdectServices
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductServices(IUnitOfWork unitOfWork , IMapper mapper) {
        
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

        public async Task<PagiationResult<ProductDTO>> GetAllProductAsync(ProductQueryParams productQuery)
        {
            var Spec = new ProductBaseSpecifications(productQuery);
            var Product = await _unitOfWork.GetRepository<Product, int>().GetAllAsync( Spec);
           var DataToReturn = _mapper.Map<IEnumerable<ProductDTO>>(Product);
            var CountDataToReturn = DataToReturn.Count();
            var CountSpec = new ProductCountSpecificaion(productQuery);
            var CountOfAllProducts =await _unitOfWork.GetRepository<Product, int>().CountAsyn(CountSpec);
            return  new  PagiationResult<ProductDTO>(productQuery.PageIndex, CountDataToReturn, CountOfAllProducts, DataToReturn);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var Spec = new ProductBaseSpecifications(id);
               var Product =await  _unitOfWork.GetRepository<Product, int>().GetByIdAsync(Spec);
            return _mapper.Map<ProductDTO>(Product);
        }

        public async Task<IEnumerable<TypeDTO>> GetTypeAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDTO>>(Types);
        }
    }
}
