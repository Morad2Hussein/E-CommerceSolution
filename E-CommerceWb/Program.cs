
using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.DataSeed;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Persistence.Repositries;
using E_Commerce.Persistence.UnitOfWork;
using E_Commerce.Services.BasketServices;
using E_Commerce.Services.CacheServices;
using E_Commerce.Services.MappingProfile;
using E_Commerce.Services.ProdectServices;
using E_Commerce.Services_Abstraction.Services;
using E_CommerceWb.CustomMiddleware;
using E_CommerceWb.Extensions;
using E_CommerceWb.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace E_CommerceWb
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(optionsAction: options =>
            {

                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataInitializer, DataInitializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //builder.Services.AddAutoMapper(x => x.AddProfile<ProductProfile>());
            builder.Services.AddAutoMapper(typeof(ServicesAssemblyReference).Assembly);
            builder.Services.AddScoped<IProductServices, ProductServices>();
            builder.Services.AddTransient<ProductPictureURLResolver>();
            builder.Services.AddSingleton<IConnectionMultiplexer>(Sp =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
            });
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped<IBasketServices, BasketServices>();
            builder.Services.AddScoped<ICacheRepository, CacheRepository>();
            builder.Services.AddScoped<ICacheServices, CacheServices>();
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.ValidationResponse;
            });


            #endregion

            var app = builder.Build();
            #region Data Seeding

            await app.MigrateDatabaseAsync();
            await app.SeedDatabaseAsync();
            #endregion
            #region Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            #endregion

            await app.RunAsync();
        }
    }
}
//PresentationLayer
