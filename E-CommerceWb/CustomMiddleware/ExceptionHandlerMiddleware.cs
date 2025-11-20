using E_Commerce.Services.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceWb.CustomMiddleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlerMiddleware(RequestDelegate Next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = Next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                await HandleNotFoundEndPoint(context);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An unhandled exception has occurred.");
                // Set the response status code and content
                var Problem = new ProblemDetails()
                {
                    Title = "An error occurred while processing your request.",
                    Detail = ex.Message,
                    Instance = context.Request.Path,
                    Status = ex switch
                    {
                          NotFoundException => StatusCodes.Status404NotFound,
                            _ => StatusCodes.Status500InternalServerError
                    }

                };
                context.Response.StatusCode = StatusCodes.Status500InternalServerError; // Internal Server Error
                await context.Response.WriteAsJsonAsync(Problem);
            }

        }
        #region Helper

        private static async Task HandleNotFoundEndPoint(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound  && !context.Response.HasStarted)
            {
                var Problem = new ProblemDetails()
                {
                    Title = "The requested resource was not found.",
                    Status = StatusCodes.Status404NotFound,
                    Detail = $"No resource found at {context.Request.Path}",
                    Instance = context.Request.Path
                };
                await context.Response.WriteAsJsonAsync(Problem);
            }
        }

        #endregion
    }
}
