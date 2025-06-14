using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Models;

namespace WebApi.Filters;

public class Shirt_ValidateUpdateShirtFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
        
        var id = context.ActionArguments["id"] as int?;
        var shirt = context.ActionArguments["shirt"] as Shirt;

        if (id.HasValue && shirt != null && id != shirt.ShirtId)
        {
            context.ModelState.AddModelError("shirtId", "Shirt id is invalid");
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest
            };
            context.Result = new BadRequestObjectResult(problemDetails);
        }
    }
}