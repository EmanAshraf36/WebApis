using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Repositories;

namespace WebApp.Controllers;

public class ShirtsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View(ShirtRepository.GetShirts());
    }
}