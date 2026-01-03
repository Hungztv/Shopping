using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Models.Repository;
using Shopping.Models;
using X.PagedList;

namespace Shopping_Tutorial.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DataContext _dataContext;
        public CategoryController(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IActionResult> Index(string slug = "", int? page = 1)
        {
            int pageNumber = page ?? 1;
            int pageSize = 9;

            CategoryModel category = await _dataContext.Categories.FirstOrDefaultAsync(c => c.Slug == slug);

            if (category == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Slug = slug;
            IQueryable<ProductModel> productsByCategory = _dataContext.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Where(p => p.CategoryId == category.Id);

            var totalCount = await productsByCategory.CountAsync();
            var items = await productsByCategory.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var pagedProducts = new X.PagedList.StaticPagedList<ProductModel>(items, pageNumber, pageSize, totalCount);

            ViewBag.count = pagedProducts.TotalItemCount;

            return View(pagedProducts);
        }
    }
}
