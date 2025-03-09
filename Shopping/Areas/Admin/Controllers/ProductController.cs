using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Shopping.Models;
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
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_dataContext.Categories,"Id","Name");
            ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductModel product)
        {
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name",product.CategoryId);
            ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name",product.BrandId);
            if (ModelState.IsValid)
            {
                TempData["success"] = "ok heets";
            }
            else
            {
                TempData["error"] = "Có lỗi xảy ra";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)

                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessages = string.Join("\n", errors);
                return BadRequest(errorMessages);
            }
           
            return View(product);
        }
    }
}
