using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Models.Repository;
using Shopping.Models;
using System.Diagnostics;

using X.PagedList.Extensions;

namespace Shopping.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUserModel> _userManager;

        public HomeController(ILogger<HomeController> logger, DataContext context, UserManager<AppUserModel> userManager)
        {
            _logger = logger;
            _dataContext = context;
            _userManager = userManager;

        }

        public IActionResult Index(int? page)
        {
            int pageSize = 6;
            int pageNumber = page ?? 1;

            var products = _dataContext.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)

                .OrderByDescending(p => p.Id)
                .ToPagedList(pageNumber, pageSize);


            var sliders = _dataContext.Sliders.Where(s => s.Status == 1).ToList();
            ViewBag.Sliders = sliders;

            return View(products);
        }
        public async Task<IActionResult> Compare()
        {
            var compare_product = await (from c in _dataContext.Compares
                                         join p in _dataContext.Products on c.ProductId equals p.Id
                                         join u in _dataContext.Users on c.UserId equals u.Id
                                         select new { User = u, Product = p, Compares = c })
                               .ToListAsync();

            return View(compare_product);
        }
        public async Task<IActionResult> DeleteCompare(int Id)
        {
            CompareModel compare = await _dataContext.Compares.FindAsync(Id);

            _dataContext.Compares.Remove(compare);

            await _dataContext.SaveChangesAsync();
            TempData["success"] = "So sánh đã được xóa thành công";
            return RedirectToAction("Compare", "Home");
        }
        public async Task<IActionResult> DeleteWishlist(int Id)
        {
            WishlistModel wishlist = await _dataContext.Wishlists.FindAsync(Id);

            _dataContext.Wishlists.Remove(wishlist);

            await _dataContext.SaveChangesAsync();
            TempData["success"] = "Yêu thích đã được xóa thành công";
            return RedirectToAction("Wishlist", "Home");
        }
        public async Task<IActionResult> Wishlist()
        {
            var wishlist_product = await (from w in _dataContext.Wishlists
                                          join p in _dataContext.Products on w.ProductId equals p.Id
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

            _dataContext.Wishlists.Add(wishlistProduct);
            try
            {
                await _dataContext.SaveChangesAsync();
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

            _dataContext.Compares.Add(compareProduct);
            try
            {
                await _dataContext.SaveChangesAsync();
                return Ok(new { success = true, message = "Add to compare Successfully" });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while adding to compare table.");
            }

        }
        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Contact()
        {
            var contact = await _dataContext.Contact.FirstOrDefaultAsync();
            if (contact == null)
            {
                // Tạo contact mặc định nếu chưa có
                contact = new ContactModel
                {
                    Name = "Shopping Store",
                    Email = "duongmanhung1210@gmail.com",
                    Phone = "+84 865322936",
                    Description = "Chúng tôi cung cấp các sản phẩm công nghệ chất lượng cao",
                    Map = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3724.4!2d105.8!3d21.0!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x0%3A0x0!2zMjHCsDAwJzAwLjAiTiAxMDXCsDQ4JzAwLjAiRQ!5e0!3m2!1sen!2s!4v1234567890"
                };
            }
            return View(contact);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statuscode)
        {
            if (statuscode == 404)
            {
                return View("NotFound");
            }
            else
            {

            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
