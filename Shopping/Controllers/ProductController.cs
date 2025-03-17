 using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Search(string searchTerm)
        {
            var products = await _datacontext.Products
            .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
            .ToListAsync();

            ViewBag.Keyword = searchTerm;

            return View(products);
        }
        public async Task<IActionResult> Details(int Id)
        {
            if (Id == null) return RedirectToAction("Index");
            var productsById = _datacontext.Products.Where(p => p.Id == Id).FirstOrDefault();
            var relatedProducts = _datacontext.Products.Where(p => p.CategoryId == productsById.CategoryId && p.Id != Id).ToList();
            ViewBag.RelatedProducts = relatedProducts;
            return View(productsById);
        }
    }
}
