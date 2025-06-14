using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Data;
using WebApi.Models;
using WebApi.Models.Repositories;

namespace WebApi.Filters.ActionFilters;

public class Shirt_ValidateCreateShirtFilterAttribute: ActionFilterAttribute
{
    private readonly ApplicationDbContext db;
    //dependcy injection of the db into the function
    public Shirt_ValidateCreateShirtFilterAttribute(ApplicationDbContext db)
    {
        this.db = db;
    }

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
            var existingShirt = db.Shirts.FirstOrDefault(x =>
                !string.IsNullOrWhiteSpace(shirt.Brand)
                && !string.IsNullOrWhiteSpace(x.Brand)
                && x.Brand.ToLower() == shirt.Brand.ToLower()
                && !string.IsNullOrWhiteSpace(shirt.Color)
                && !string.IsNullOrWhiteSpace(x.Color)
                && x.Color.ToLower() == shirt.Color.ToLower()
                && !string.IsNullOrWhiteSpace(shirt.Gender)
                && !string.IsNullOrWhiteSpace(x.Gender)
                && x.Gender.ToLower() == shirt.Gender.ToLower()
                && shirt.Size.HasValue
                && x.Size.HasValue
                && shirt.Size.Value == x.Size.Value);
            
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