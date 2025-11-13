using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopping.Models.Repository;
using Shopping.Models;


namespace Shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Product")]
    [Authorize(Roles = "Admin")]


    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public ProductController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = context;
            _webHostEnviroment = webHostEnvironment;
        }

        [Route("Index")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Products.OrderByDescending(p => p.Id).Include(c => c.Category).Include(b => b.Brand).ToListAsync());
        }

        [Route("CreateProductQuantity")]
        [HttpGet]
        public async Task<IActionResult> CreateProductQuantity(long Id)
        {
            var productbyquantity = await _dataContext.ProductQuantities.Where(pq => pq.ProductId == Id).ToListAsync();
            ViewBag.ProductByQuantity = productbyquantity;
            ViewBag.ProductId = Id;
            return View();
        }

        [Route("UpdateMoreQuantity")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateMoreQuantity(ProductQuantityModel productQuantityModel)
        {
            // Get the product to update
            var product = _dataContext.Products.Find(productQuantityModel.ProductId);

            if (product == null)
            {
                return NotFound(); // Handle product not found scenario
            }
            product.Quantity += productQuantityModel.Quantity;

            productQuantityModel.Quantity = productQuantityModel.Quantity;
            productQuantityModel.ProductId = productQuantityModel.ProductId;
            productQuantityModel.Dateceated = DateTime.Now;


            _dataContext.Add(productQuantityModel);
            _dataContext.SaveChangesAsync();
            TempData["success"] = "Thêm số lượng sản phẩm thành công";
            return RedirectToAction("CreateProductQuantity", "Product", new { Id = productQuantityModel.ProductId });
        }


        [Route("Create")]

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name");
            ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name");
            return View();
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel product)
        {
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandId);

            if (ModelState.IsValid)
            {
                product.Slug = product.Name.Replace(" ", "-");
                var slug = await _dataContext.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Sản phẩm đã có trong database");
                    return View(product);
                }

                if (product.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnviroment.WebRootPath, "media/products");
                    string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                    product.Image = imageName;
                }

                _dataContext.Add(product);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Thêm sản phẩm thành công";
                return RedirectToAction("Index");

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
                return BadRequest(errorMessage);
            }
            return View(product);
        }


        [Route("Edit")]

        public async Task<IActionResult> Edit(int Id)
        {
            ProductModel product = await _dataContext.Products.FindAsync(Id);
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandId);

            return View(product);
        }

        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductModel product)
        {
            var existed_product = _dataContext.Products.Find(product.Id); //tìm sp theo id product
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandId);

            if (ModelState.IsValid)
            {
                product.Slug = product.Name.Replace(" ", "-");

                if (product.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnviroment.WebRootPath, "media/products");
                    string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                    existed_product.Image = imageName;
                }
                // Update other product properties
                existed_product.Name = product.Name;
                existed_product.Description = product.Description;
                existed_product.CapitalPrice = product.CapitalPrice;
                existed_product.Price = product.Price;
                existed_product.CategoryId = product.CategoryId;
                existed_product.BrandId = product.BrandId;
                // ... other properties
                _dataContext.Update(existed_product);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Cập nhật sản phẩm thành công";
                return RedirectToAction("Index");
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
                return BadRequest(errorMessage);
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                ProductModel product = await _dataContext.Products.FindAsync(Id);

                if (product == null)
                {
                    TempData["error"] = "Sản phẩm không tồn tại";
                    return RedirectToAction("Index");
                }

                // Remove related data first to avoid foreign key conflicts
                // Remove ratings
                var ratings = await _dataContext.Ratings.Where(r => r.ProductId == Id).ToListAsync();
                if (ratings.Any())
                {
                    _dataContext.Ratings.RemoveRange(ratings);
                }

                // Remove wishlists
                var wishlists = await _dataContext.Wishlists.Where(w => w.ProductId == Id).ToListAsync();
                if (wishlists.Any())
                {
                    _dataContext.Wishlists.RemoveRange(wishlists);
                }

                // Remove compares
                var compares = await _dataContext.Compares.Where(c => c.ProductId == Id).ToListAsync();
                if (compares.Any())
                {
                    _dataContext.Compares.RemoveRange(compares);
                }

                // Remove product quantities
                var productQuantities = await _dataContext.ProductQuantities.Where(pq => pq.ProductId == Id).ToListAsync();
                if (productQuantities.Any())
                {
                    _dataContext.ProductQuantities.RemoveRange(productQuantities);
                }

                // Check if product is in any orders
                var orderDetails = await _dataContext.OrderDetails.Where(od => od.ProductId == Id).AnyAsync();
                if (orderDetails)
                {
                    TempData["error"] = "Không thể xóa sản phẩm này vì đã có trong đơn hàng. Bạn có thể đặt số lượng về 0 thay vì xóa.";
                    return RedirectToAction("Index");
                }

                // Delete product image if it exists
                if (!string.IsNullOrEmpty(product.Image) && !string.Equals(product.Image, "noname.jpg"))
                {
                    string uploadsDir = Path.Combine(_webHostEnviroment.WebRootPath, "media/products");
                    string oldfilePath = Path.Combine(uploadsDir, product.Image);
                    if (System.IO.File.Exists(oldfilePath))
                    {
                        System.IO.File.Delete(oldfilePath);
                    }
                }

                // Finally remove the product
                _dataContext.Products.Remove(product);
                await _dataContext.SaveChangesAsync();

                TempData["success"] = "Sản phẩm đã được xóa thành công";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Lỗi khi xóa sản phẩm: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
