using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopping.Models.Repository;

namespace Shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;
        public ProductController(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IActionResult> Index()
        {


            return View(await _dataContext.Products
                .OrderByDescending(p => p.Id) // Đúng cú pháp
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .ToListAsync()); // Đúng chữ 'A' viết hoa);
        }
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_dataContext.Categories,"Id","Name");
            ViewBag.Brands = new SelectList(_dataContext.Categories, "Id", "Name");
            return View();
        }
    }
}
