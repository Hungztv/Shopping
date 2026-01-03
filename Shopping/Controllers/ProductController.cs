using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Models;
using Shopping.Models.Repository;
using Shopping.Models.ViewModels;
using X.PagedList;

namespace Shopping.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _datacontext;
        public ProductController(DataContext context)
        {
            _datacontext = context;
        }
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 6; // Or any other page size you prefer
            var productsQuery = _datacontext.Products
                                             .Include(p => p.Category)
                                             .Include(p => p.Brand)
                                             .OrderByDescending(p => p.Id);
            var totalCount = await productsQuery.CountAsync();
            var items = await productsQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var products = new X.PagedList.StaticPagedList<ProductModel>(items, pageNumber, pageSize, totalCount);
            return View(products);
        }

        public async Task<IActionResult> Search(string searchTerm, int? page)
        {
            ViewBag.Keyword = searchTerm;
            var pageNumber = page ?? 1;
            var pageSize = 6;

            if (string.IsNullOrEmpty(searchTerm))
            {
                // If search term is empty, return to the main product index
                return RedirectToAction("Index");
            }

            var productsQuery = _datacontext.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));
            var totalCount = await productsQuery.CountAsync();
            var items = await productsQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var products = new X.PagedList.StaticPagedList<ProductModel>(items, pageNumber, pageSize, totalCount);

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
