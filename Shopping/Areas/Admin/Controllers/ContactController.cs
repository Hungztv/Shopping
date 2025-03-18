using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shopping.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Route("Admin/Contact")]
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
