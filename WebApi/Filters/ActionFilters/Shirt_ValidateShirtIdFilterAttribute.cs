using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApi.Data;
using WebApi.Models.Repositories;

namespace WebApi.Filters;

public class Shirt_ValidateShirtIdFilterAttribute : ActionFilterAttribute
{
    private readonly ApplicationDbContext db;

    public Shirt_ValidateShirtIdFilterAttribute(ApplicationDbContext db)
    {
        this.db = db;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var shirtId = context.ActionArguments["id"] as int?;
        if (shirtId.HasValue)
        {
            if (shirtId.Value <= 0)
            {
                context.ModelState.AddModelError("ShirtId","ShirtId is invalid");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
            else 
            {
                var shirt = db.Shirts.Find(shirtId.Value);
                
                if (shirt == null)
                {
                    context.ModelState.AddModelError("ShirtId", $"Shirt {shirtId.Value} is invalid");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new NotFoundObjectResult(problemDetails);
                }
                else
                {
                    context.HttpContext.Items["shirt"] = shirt;
                }
            }
        }
    }
}