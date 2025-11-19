using Microsoft.AspNetCore.Mvc;

namespace E_CommerceWb.Factories
{
    public static class ApiResponseFactory 
    {
        public static IActionResult ValidationResponse(ActionContext actionContext) {

            var errors = actionContext.ModelState
                           .Where(e => e.Value.Errors.Count > 0)
                           .ToDictionary(
                               k => k.Key,
                               k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                           );

            var problemDetails = new ValidationProblemDetails()
            {
                Title = "Validation Errors",
                Detail = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest
            };

            problemDetails.Extensions.Add("errors", errors);

            return new BadRequestObjectResult(problemDetails);

        }
    }
}
