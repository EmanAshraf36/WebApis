using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
//placed even before the class 
[ApiController]
[Route("api/[controller]")]

public class ShirtsController : ControllerBase
{
    [HttpGet]
    public string GetShirts()
    {
        return "reading all the shirts";
    }
    
    [HttpGet("{id}")]
    public string GetShirtById(int id)
    {
        return $"reading shirts by the id: {id}";
    }

}