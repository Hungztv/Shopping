using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Models;
using Shopping.Models.Repository;

namespace Shopping.Controllers
{

    public class HomeController : Controller
    {
        private readonly DataContext _datacontext;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUserModel> _userManager;

        public HomeController(ILogger<HomeController> logger, DataContext context, UserManager<AppUserModel> userManager)
        {
            _logger = logger;
            _datacontext = context;
            _userManager = userManager;

        }


        public IActionResult Index()
        {
            var products = _datacontext.Products.Include("Category").Include("Brand").ToList();
            var sliders = _datacontext.Sliders.Where(s => s.Status == 1).ToList();
            ViewBag.Sliders = sliders;
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contact()
        {
            
            return View();
        }

        public async Task<IActionResult> Compare()
        {
            var compare_product = await (from c in _datacontext.Compares
                                         join p in _datacontext.Products on c.ProductId equals p.Id
                                         join u in _datacontext.Users on c.UserId equals u.Id
                                         select new { User = u, Product = p, Compares = c })
                               .ToListAsync();

            return View(compare_product);
        }
        public async Task<IActionResult> DeleteCompare(int Id)
        {
            CompareModel compare = await _datacontext.Compares.FindAsync(Id);

            _datacontext.Compares.Remove(compare);

            await _datacontext.SaveChangesAsync();
            TempData["success"] = "So sánh đã được xóa thành công";
            return RedirectToAction("Compare", "Home");
        }
        public async Task<IActionResult> DeleteWishlist(int Id)
        {
            WishlistModel wishlist = await _datacontext.Wishlists.FindAsync(Id);

            _datacontext.Wishlists.Remove(wishlist);

            await _datacontext.SaveChangesAsync();
            TempData["success"] = "Yêu thích đã được xóa thành công";
            return RedirectToAction("Wishlist", "Home");
        }
        public async Task<IActionResult> Wishlist()
        {
            var wishlist_product = await (from w in _datacontext.Wishlists
                                          join p in _datacontext.Products on w.ProductId equals p.Id
                                          select new { Product = p, Wishlists = w })
                               .ToListAsync();

            return View(wishlist_product);
        }

        [HttpPost]
        public async Task<IActionResult> AddWishlist(int Id, WishlistModel wishlistmodel)
        {
            var user = await _userManager.GetUserAsync(User);

            var wishlistProduct = new WishlistModel
            {
                ProductId = Id,
                UserId = user.Id
            };

            _datacontext.Wishlists.Add(wishlistProduct);
            try
            {
                await _datacontext.SaveChangesAsync();
                return Ok(new { success = true, message = "Add to wishlisht Successfully" });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while adding to wishlist table.");
            }

        }
        [HttpPost]
        public async Task<IActionResult> AddCompare(int Id)
        {
            var user = await _userManager.GetUserAsync(User);

            var compareProduct = new CompareModel
            {
                ProductId = Id,
                UserId = user.Id
            };

            _datacontext.Compares.Add(compareProduct);
            try
            {
                await _datacontext.SaveChangesAsync();
                return Ok(new { success = true, message = "Add to compare Successfully" });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while adding to compare table.");
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statuscode)
        {
            if(statuscode == 404)
            {
                return View("NotFound");

            }
            else
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
           
        }
    }
}