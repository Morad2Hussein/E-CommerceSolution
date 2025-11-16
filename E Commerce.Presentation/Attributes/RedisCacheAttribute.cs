using E_Commerce.Services_Abstraction.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace E_Commerce.Presentation.Attributes
{
    public class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int _durationInMint;
        public RedisCacheAttribute(int DurationInMint = 5) {
         _durationInMint = DurationInMint ;
        }
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            // Get the cache service from the dependency injection container
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheServices>();
            // Create a unique cache key based on the request path and query string
            var cacheKey = GenerateCacheKey(context.HttpContext.Request);
            //Check if the response is already cached
            var cachedResponse = await cacheService.GetAsync(cacheKey);
                // If cached response exists, write it to the response and skip the action execution
            if ( cachedResponse is not null)
            {
                context.Result =  new ContentResult()
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                return;
            }
            // Proceed with the action execution
            var ExecutedContext = await next.Invoke();
            if ( ExecutedContext.Result is ObjectResult objectResult)
            {
                // Cache the response for future requests
                await cacheService.SetAsync(cacheKey, objectResult.Value, TimeSpan.FromMinutes(_durationInMint));
            }
        }
        #region Helper 
        private string GenerateCacheKey(HttpRequest request)
        {
           StringBuilder keyBuilder = new StringBuilder();
            keyBuilder.Append(request.Path);
            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }
            return keyBuilder.ToString();
        }
        #endregion
    }
}
