using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Data;
using WebApi.Models.Repositories;

namespace WebApi.Filters.ExceptionFilters;

public class Shirt_HandleUpdateExceptionsFilterAttribute: ExceptionFilterAttribute
{
    private readonly ApplicationDbContext db;
    public Shirt_HandleUpdateExceptionsFilterAttribute(ApplicationDbContext db)
    {
        this.db = db;
    }
    
    public override void OnException(ExceptionContext context)
    {
        base.OnException(context);
        
        var strShirtId = context.RouteData.Values["id"] as string;
        
        if (int.TryParse(strShirtId, out int shirtId))
        {
            if (db.Shirts.FirstOrDefault(x => x.ShirtId == shirtId ) == null)
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