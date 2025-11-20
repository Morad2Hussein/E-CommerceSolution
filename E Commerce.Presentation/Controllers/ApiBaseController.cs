using E_Commerce.Shared.ComonResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ApiBaseController : ControllerBase
    {
        //Heandler Result Without Value
        // If result IsSuccess Return  NoContent (204)
        // If result IsFailure Return  Appropriate Status Code With Errors

        protected IActionResult HandleResult(Shared.ComonResults.Results result)
        {
            if (result.IsSuccess)
                return NoContent(); // 204 No Content
            else 
                               return HandleProblem(result.Errors);
        }
        //Heandler Result Witho Value
        // If result IsSuccess Return  Ok (200) With Value
        // If result IsFailure Return  Appropriate Status Code With Errors

        protected ActionResult<T> HandleResult<T>(Results<T> result)
        {
            if (result.IsSuccess)
                return Ok(result.Value); // 200 OK with Value
            else
                return HandleProblem(result.Errors);

        }


        private ActionResult HandleProblem(IReadOnlyList<Errors> errors)
        {
            if (errors.Count == 0)
                return Problem(
                    title: "An unexpected error occurred.",
                    statusCode: StatusCodes.Status500InternalServerError
                );
            if (errors.All(e=>e.Types == ErrorTypes.Vaildation))
                return HandleValidationProblem(errors);
            return HandleSingleErrorProblem(errors[0]);

        }
        private ActionResult HandleSingleErrorProblem(Errors error)
        {
            return Problem(
                title: error.Code,
                detail: error.Description,
                type: error.Types.ToString(),
                statusCode: MapErrorToStatusCode(error.Types)
            );

        }

        private static int MapErrorToStatusCode(ErrorTypes types) => types switch
        {
            ErrorTypes.Failure => StatusCodes.Status500InternalServerError,
            ErrorTypes.Vaildation => StatusCodes.Status400BadRequest,
            ErrorTypes.NotFound => StatusCodes.Status404NotFound,
            ErrorTypes.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorTypes.Forbidden => StatusCodes.Status403Forbidden,
            ErrorTypes.InValidCredentials => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };  
        private ActionResult HandleValidationProblem(IReadOnlyList<Errors> errors)
        {
            var modelState = new ModelStateDictionary();
            foreach (var error in errors)

                modelState.AddModelError(error.Code, error.Description);
             return ValidationProblem(modelState);
        }
    }
}
