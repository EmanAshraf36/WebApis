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
        return Ok("reading all the shirts");
    }
    
    [HttpGet("{id}")]
    [Shirt_ValidateShirtIdFilter]
    public IActionResult GetShirtById(int id)
    {
        return Ok(ShirtRepository.GetShirtById(id));
    }

    [HttpPost]
    public IActionResult CreateShirt([FromBody] Shirt shirt)
    {
        return Ok("creating a new shirt");
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