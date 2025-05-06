using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models.Repositories;
using WebApp.Models;

namespace WebApp.Controllers;

public class ShirtsController : Controller
{
    private readonly IWebApiExecutor webApiExecutor;

    public ShirtsController(IWebApiExecutor webApiExecutor)
    {
        this.webApiExecutor = webApiExecutor;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        return View(await webApiExecutor.InvokeGet<List<Shirt>>("shirts"));
    }
    //Await: waiting for results of the API
    //Async: multiple things run at the same time  

    public IActionResult CreateShirt()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateShirt(Shirt shirt)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var response = await webApiExecutor.InvokePost("shirts", shirt);
                if (response != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (WebApiException ex)
            {
                HandleWebApiException(ex);
            }
        }
        return View(shirt);
    }
    
    public async Task<IActionResult> UpdateShirt(int shirtId)
    {
        try
        {
            //var shirt = await WebApiExecutor.InvokeGet<Shirt>($"shirts/{shirtId}");
            var shirt = await webApiExecutor.InvokeGet<Shirt>($"shirts/{shirtId}");
            if (shirt != null)
            {
                return View(shirt);
            }
        }
        catch (WebApiException ex)
        {
            HandleWebApiException(ex);
            return View();
        }

        return NotFound();
    }

    [HttpPost]

    public async Task<IActionResult> UpdateShirt(Shirt shirt)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await webApiExecutor.InvokePut($"shirts/{shirt.ShirtId}", shirt);
                return RedirectToAction(nameof(Index));
            }
            catch (WebApiException ex)
            {
                HandleWebApiException(ex);
            }
        }
        return View(shirt);
    }
    
    private void HandleWebApiException(WebApiException ex)
    {
        //why did I have to put those three Conditions? why not just 1?
        if (ex.ErrorResponse != null &&
            ex.ErrorResponse.Errors != null &&
            ex.ErrorResponse.Errors.Count > 0)
        {
            foreach (var error in ex.ErrorResponse.Errors)
            {
                ModelState.AddModelError(error.Key, string.Join("; ", error.Value));
            }
        }
    }


}