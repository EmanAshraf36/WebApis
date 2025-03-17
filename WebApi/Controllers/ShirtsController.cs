using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Models;
using WebApi.Models.Repositories;

namespace WebApi.Controllers;
//placed even before the class 
[ApiController]
[Route("api/[controller]")]

public class ShirtsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetShirts()
    {
        return Ok(ShirtRepository.GetShirts());
    }
    
    [HttpGet("{id}")]
    [Shirt_ValidateShirtIdFilter]
    public IActionResult GetShirtById(int id)
    {
        return Ok(ShirtRepository.GetShirtById(id));
    }

    [HttpPost]
    [Shirt_ValidateCreateShirtFilter]
    public IActionResult CreateShirt([FromBody] Shirt shirt)
    {
        ShirtRepository.AddShirt(shirt);
        return CreatedAtAction(nameof(GetShirtById),
            new{ id = shirt.ShirtId}, shirt);
    }

    [HttpPut]
    public IActionResult UpdateShirt(int id)
    {
        return Ok($"updating a shirt {id}");
    }

    [HttpDelete("{id}")]

    public IActionResult DeleteShirt(int id)
    {
        return Ok($"deleting a shirt {id}");
    }

}