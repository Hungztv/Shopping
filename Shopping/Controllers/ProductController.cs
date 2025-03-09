using Microsoft.AspNetCore.Mvc;
using Shopping.Models.Repository;

namespace Shopping.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _datacontext;
        public ProductController (DataContext context)
        {
            _datacontext = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(int Id = 0)
        {
            if (Id == null) return RedirectToAction("Index");
            var productsById = _datacontext.Products.Where(p => p.Id == Id).FirstOrDefault();
            return View(productsById);
        }
    }
}
