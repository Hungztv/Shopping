using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shopping.Models;
using Shopping.Models.Repository;
using Shopping.Models.ViewModels;

namespace Shopping.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext _datacontext;
        public CartController(DataContext _context)
        {
            _datacontext = _context;
        }

        public IActionResult Index()
        {
            List<CartItemModel> cartItem = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            // Nhận shipping giá từ cookie
            var shippingPriceCookie = Request.Cookies["ShippingPrice"];
            decimal shippingPrice = 0;

            if (shippingPriceCookie != null)
            {
                var shippingPriceJson = shippingPriceCookie;
                shippingPrice = JsonConvert.DeserializeObject<decimal>(shippingPriceJson);
            }
            CartItemViewModel cartVM = new()
            {
                CartItems = cartItem,
                GrandTotal = cartItem.Sum(x => x.Quantity * x.Price),
                ShippingPrice = shippingPrice
            };
            return View(cartVM);
        }

        public ActionResult Checkout()
        {
            return View("~/View/Checkout/Index.cshtml");
        }
        public async Task<IActionResult> Add(int Id)
        {
            ProductModel product = await _datacontext.Products.FindAsync(Id);

            if (product == null)
            {
                return NotFound("Product not found");
            }

            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemModel cartItem = cart.FirstOrDefault(c => c.ProductId == Id);

            if (cartItem == null)
            {
                cart.Add(new CartItemModel(product));
            }
            else
            {
                cartItem.Quantity ++;
            }
            TempData["success"] = "add product quantity successfully!";
            HttpContext.Session.SetJson("Cart", cart);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> Decrease(int Id)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

            CartItemModel cartItem = cart.FirstOrDefault(c => c.ProductId == Id);

            if (cartItem != null)
            {
                if (cartItem.Quantity >= 1)
                {
                    --cartItem.Quantity;
                }
                else
                {
                    cart.RemoveAll(p => p.ProductId == Id);
                }

                if (cart.Count == 0)
                {
                    HttpContext.Session.Remove("Cart");
                }
                else
                {
                    HttpContext.Session.SetJson("Cart", cart);
                }

                TempData["success"] = "Decreased product quantity successfully!";
            }

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Increase(int Id)
        {
            ProductModel product = await _datacontext.Products.Where(p => p.Id == Id).FirstOrDefaultAsync();

            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
            CartItemModel cartItem = cart.Where(c => c.ProductId == Id).FirstOrDefault();
            if (cartItem.Quantity >= 1 && product.Quantity > cartItem.Quantity)
            {
                ++cartItem.Quantity;
                TempData["success"] = "Increase Product to cart Sucessfully! ";
            }
            else
            {
                cartItem.Quantity = product.Quantity;
                TempData["success"] = "Maximum Product Quantity to cart Sucessfully! ";

                //cart.RemoveAll(p => p.ProductId == Id);
            }
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(int Id)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

            if (cart.Any(c => c.ProductId == Id))
            {
                cart.RemoveAll(p => p.ProductId == Id);

                if (cart.Count == 0)
                {
                    HttpContext.Session.Remove("Cart");
                }
                else
                {
                    HttpContext.Session.SetJson("Cart", cart);
                }

                TempData["success"] = "Removed product from cart successfully!";
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Clear()
        {
            HttpContext.Session.Remove("Cart");

            TempData["success"] = "Clear all Product to cart Sucessfully! ";
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("Cart/GetShipping")]
        public async Task<IActionResult> GetShipping(ShippingModel shippingModel, string quan, string tinh, string phuong)
        {

            var existingShipping = await _datacontext.Shippings
                .FirstOrDefaultAsync(x => x.City == tinh && x.District == quan && x.Ward == phuong);

            decimal shippingPrice = 0; // Set mặc định giá tiền

            if (existingShipping != null)
            {
                shippingPrice = existingShipping.Price;
            }
            else
            {
                //Set mặc định giá tiền nếu ko tìm thấy
                shippingPrice = 50000;
            }
            var shippingPriceJson = JsonConvert.SerializeObject(shippingPrice);
            try
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(30),
                    Secure = true // using HTTPS
                };

                Response.Cookies.Append("ShippingPrice", shippingPriceJson, cookieOptions);
            }
            catch (Exception ex)
            {
            
                Console.WriteLine($"Error adding shipping price cookie: {ex.Message}");
            }
            return Json(new { shippingPrice });
        }
        [HttpPost]
        [Route("Cart/RemoveShippingCookie")]
        public IActionResult RemoveShippingCookie()
        {
            Response.Cookies.Delete("ShippingPrice");
            return Json(new { success = true });
        }
    }

}
