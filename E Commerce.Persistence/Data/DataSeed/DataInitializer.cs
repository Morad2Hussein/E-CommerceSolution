

using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.Product_Module;
using E_Commerce.Domain.Entities.Shared;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.DataSeed
{
    public class DataInitializer : IDataInitializer
    {
        private readonly StoreDbContext _dbContext;
        public DataInitializer(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task InitializeAsync()
        {
            try
            {
                #region   check if i have data in the database or not  in the Product - ProductTypes - ProductBrands 


                var HasProduct =await _dbContext.Products.AnyAsync();

                var HasProductBrand =await _dbContext.ProductBrands.AnyAsync();

                var HasProductType =await  _dbContext.ProductTypes.AnyAsync();

                if (HasProduct && HasProductBrand && HasProductType) return;
                #endregion
                #region i not have data
                if (!HasProductBrand)
                {
                await    SeedDataFromJsonAsync<ProductBrand, int>("brands.json", _dbContext.ProductBrands);
                }
                if (!HasProductType)
                {
                   await SeedDataFromJsonAsync<ProductType, int>("types.json", _dbContext.ProductTypes);
                   await _dbContext.SaveChangesAsync();
                }
                if (!HasProduct)
                {
                 await   SeedDataFromJsonAsync<Product, int>("products.json", _dbContext.Products);
                 await   _dbContext.SaveChangesAsync();
                }
                #endregion
            }
            catch (Exception ex)

            {
                Console.WriteLine($"Data Seeding Filed {ex}");

            }


        }

        #region HelperMethods
        // I want to take two Parameters 
        // the first is the fileName the second is the DbSet
        private async Task SeedDataFromJsonAsync<T, TKey>(string fileName, DbSet<T> dbset) where T : BaseEntity<TKey>
        {
            // check if the file is available or not

            //F:\pro\E-CommerceSolution\E Commerce.Persistence\Data\DataSeed\JSONFiles\

            var FilePath = @"..\E Commerce.Persistence\Data\DataSeed\JSONFiles\" + fileName;
            // check if the file is available or not
            if (!File.Exists(FilePath)) throw new FileNotFoundException($"file {fileName} is not Exist");
            try
            {
                // open stream 
                using var dataStream = File.OpenRead(FilePath);
                var data =await JsonSerializer.DeserializeAsync<List<T>>(dataStream, new JsonSerializerOptions()
                {

                    PropertyNameCaseInsensitive = true

                });
                // check if data is null or not 
                if (data is not null)
                {
                  await  dbset.AddRangeAsync(data);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error while Reading The JsonFile {ex}");
            }

        }
        #endregion
    }

}
