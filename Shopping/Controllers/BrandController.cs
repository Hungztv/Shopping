using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Models;
using Shopping.Models.Repository;
using X.PagedList;

namespace Shopping.Controllers
{
    public class BrandController : Controller
    {
        private readonly DataContext _datacontext;
        public BrandController(DataContext context)
        {
            _datacontext = context;
        }
        public async Task<IActionResult> Index(string Slug = "", int? page = 1)
        {
            int pageNumber = page ?? 1;
            int pageSize = 9;

            BrandModel brand = await _datacontext.Brands.FirstOrDefaultAsync(x => x.Slug == Slug);
            if (brand == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Slug = Slug;

            var productsByBrand = _datacontext.Products
                                                .Include(p => p.Category)
                                                .Include(p => p.Brand)
                                                .Where(p => p.BrandId == brand.Id);

            var totalCount = await productsByBrand.CountAsync();
            var items = await productsByBrand.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var pagedProducts = new X.PagedList.StaticPagedList<ProductModel>(items, pageNumber, pageSize, totalCount);

            return View(pagedProducts);
        }
    }
}
