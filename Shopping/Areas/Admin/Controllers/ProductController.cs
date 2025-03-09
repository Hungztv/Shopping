using Microsoft.AspNetCore.Mvc;
using Shopping.Models.Repository;

namespace Shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly DataContext _datacontext;
        public ProductController(DataContext context)
        {
            _datacontext = context;
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}
