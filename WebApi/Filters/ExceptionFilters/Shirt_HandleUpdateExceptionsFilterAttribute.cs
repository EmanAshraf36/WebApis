using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Models.Repositories;

namespace WebApi.Filters.ExceptionFilters;

public class Shirt_HandleUpdateExceptionsFilterAttribute: ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        base.OnException(context);
        
        var strShirtId = context.RouteData.Values["id"] as string;
        if (int.TryParse(strShirtId, out int shirtId))
        {
            if (!ShirtRepository.ShirtExists(shirtId))
            {
                context.ModelState.AddModelError("ShirtId", "Shirt not found");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status404NotFound
                };
                context.Result = new NotFoundObjectResult(problemDetails);


            }
        }
    }
}