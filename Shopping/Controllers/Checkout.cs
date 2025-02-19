using Microsoft.AspNetCore.Mvc;

namespace Shopping.Controllers
{
    public class Checkout : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}