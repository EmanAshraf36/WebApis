using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Models;
using WebApi.Models.Repositories;

namespace WebApi.Filters;

public class Shirt_ValidateCreateShirtFilterAttribute: ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
        var shirt = context.ActionArguments["shirt"] as Shirt;

        if (shirt == null)
        {
            context.ModelState.AddModelError("Shirt", "Shirt is null");
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest
            };
            context.Result = new BadRequestObjectResult(problemDetails);
        }
        else
        {
            var existingShirt = ShirtRepository.GetShirtByProperties(shirt.Brand,shirt.Gender, shirt.Color, shirt.Size);
            if (existingShirt != null)
            {
                context.ModelState.AddModelError("Shirt", "Shirt already exists");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}