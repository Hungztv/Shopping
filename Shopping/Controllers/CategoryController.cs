using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Models;
using Shopping.Models.Repository;

namespace Shopping.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DataContext _datacontext;
        public CategoryController(DataContext context)
        {
            _datacontext = context;
        }
        public async  Task<IActionResult> Index(string Slug="")
        {
            CategoryModel category = _datacontext.Categories.Where(x => x.Slug == Slug).FirstOrDefault();
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            var productsByCategory = _datacontext.Products.Where(p => p.Category.Id == category.Id);
            return View(await productsByCategory.OrderByDescending(p => p.Id).ToListAsync());
        }
    }
}
