 using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Models;
using Shopping.Models.Repository;
using Shopping.Models.ViewModels;

namespace Shopping.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _datacontext;
        public ProductController(DataContext context)
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
            var productsById = _datacontext.Products.Include(p => p.Ratings).Where(p => p.Id == Id).FirstOrDefault();
            var relatedProducts = _datacontext.Products.Where(p => p.CategoryId == productsById.CategoryId && p.Id != Id).ToList();
            ViewBag.RelatedProducts = relatedProducts;
            var viewModel = new ProductDetailsViewModel
            {
                ProductDetails = productsById,
               
            };
            return View(viewModel);
        }
        public async Task<IActionResult> CommentProduct(RatingModel rating)
        {
            if (ModelState.IsValid)
            {

                var ratingEntity = new RatingModel
                {
                    ProductId = rating.ProductId,
                    Name = rating.Name,
                    Email = rating.Email,
                    Comment = rating.Comment,
                    Star = rating.Star

                };

                _datacontext.Ratings.Add(ratingEntity);
                await _datacontext.SaveChangesAsync();

                TempData["success"] = "Thêm đánh giá thành công";

                return Redirect(Request.Headers["Referer"]);
            }
            else
            {
                TempData["error"] = "Model có một vài thứ đang lỗi";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("\n", errors);

                return RedirectToAction("Detail", new { id = rating.ProductId });
            }

            return Redirect(Request.Headers["Referer"]);
        }
    }
    }
