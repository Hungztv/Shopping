
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Models.Repository;
namespace Shopping.Areas.Admin.Controllers
{
  
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly DataContext _dataContext;
        public CategoryController(DataContext context) 
        {
            _dataContext = context;
            
        }
        public async Task<IActionResult> Index()
        {


            return View(await _dataContext.Categories
                .OrderByDescending(p => p.Id) // Đúng cú pháp
                .ToListAsync()); // Đúng chữ 'A' viết hoa);
        }
    }
}
