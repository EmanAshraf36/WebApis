using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Filters;
using WebApi.Filters.ActionFilters;
using WebApi.Filters.ExceptionFilters;
using WebApi.Models;
using WebApi.Models.Repositories;

namespace WebApi.Controllers;
//placed even before the class 
[ApiController]
[Route("api/[controller]")]

public class ShirtsController : ControllerBase
{
    private readonly ApplicationDbContext db;
    public ShirtsController(ApplicationDbContext db)
    {
        this.db = db;
    }

    [HttpGet]
    public IActionResult GetShirts()
    {
        return Ok(db.Shirts.ToList());
    }
    
    [HttpGet("{id}")]
    [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
    public IActionResult GetShirtById(int id)
    {
        return Ok(HttpContext.Items["shirt"]);
    }

    [HttpPost]
    [TypeFilter(typeof(Shirt_ValidateCreateShirtFilterAttribute))]
    public IActionResult CreateShirt([FromBody] Shirt shirt)
    {
        this.db.Shirts.Add(shirt);
        this.db.SaveChanges();
        
        return CreatedAtAction(nameof(GetShirtById),
            new{ id = shirt.ShirtId}, shirt);
    }

    [HttpPut("{id}")]
    [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
    [Shirt_ValidateUpdateShirtFilter]
    [TypeFilter(typeof(Shirt_HandleUpdateExceptionsFilterAttribute))]
    public IActionResult UpdateShirt(int id, Shirt shirt)
    {
        //we will take the shirt from the "ValidateShirtIdFilterAttribute"
        var shirtToUpdate = HttpContext.Items["shirt"] as Shirt;
        shirtToUpdate.Brand = shirt.Brand;
        shirtToUpdate.Color = shirt.Color;
        shirtToUpdate.Size = shirt.Size;
        shirtToUpdate.Price = shirt.Price;
        shirtToUpdate.Gender = shirt.Gender;

        db.SaveChanges();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]

    public IActionResult DeleteShirt(int id)
    {
        var shirtToDelete = HttpContext.Items["shirt"] as Shirt;
        
        db.Shirts.Remove(shirtToDelete);
        db.SaveChanges();
        
        return Ok(shirtToDelete);
    }

}