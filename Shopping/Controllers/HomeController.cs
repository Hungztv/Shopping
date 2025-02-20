using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shopping.Models;
using Shopping.Models.Repository;

namespace Shopping.Controllers;

public class HomeController : Controller
{
    private readonly DataContext _Datacontext;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger , DataContext context)
    {
        _logger = logger;
        _Datacontext = context;
    }

    public IActionResult Index()
    {
        var products = _Datacontext.Products.ToList();
        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
