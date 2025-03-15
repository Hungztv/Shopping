using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Models.Repository;

namespace Shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/User")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly DataContext _dataContext;
        public UserController(DataContext context)
        {
            _dataContext = context;

        }
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Users.OrderByDescending(p => p.Id).ToListAsync());
        }
    }
}
