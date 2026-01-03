using Shopping_Tutorial.Areas.Admin.Repository;
using Microsoft.AspNetCore.Mvc;
using Shopping.Models;
using Shopping.Models.Repository;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shopping.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Shopping.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly DataContext _context;
        private readonly IEmailSender _emailSender;
        public CheckoutController(DataContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

            if (cartItems.Count == 0)
            {
                TempData["error"] = "Giỏ hàng của bạn đang trống!";
                return RedirectToAction("Index", "Cart");
            }

            var shippingPriceCookie = Request.Cookies["ShippingPrice"];
            decimal shippingPrice = 0;
            if (shippingPriceCookie != null)
            {
                shippingPrice = JsonConvert.DeserializeObject<decimal>(shippingPriceCookie);
            }
            if (shippingPrice == 0)
            {
                TempData["error"] = "Vui lòng tính phí vận chuyển tại trang giỏ hàng trước.";
                return RedirectToAction("Index", "Cart");
            }

            var couponCode = Request.Cookies["CouponTitle"];
            decimal discountAmount = 0m;
            decimal subtotal = cartItems.Sum(i => i.Price * i.Quantity);

            if (!string.IsNullOrWhiteSpace(couponCode))
            {
                var coupon = _context.Coupons.FirstOrDefault(c => c.Name == couponCode);
                if (coupon != null)
                {
                    if (coupon.IsPercent)
                    {
                        discountAmount = Math.Round(subtotal * (coupon.DiscountValue / 100m), 2);
                    }
                    else
                    {
                        discountAmount = coupon.DiscountValue;
                    }
                    if (discountAmount > subtotal) discountAmount = subtotal;
                }
            }

            var cartViewModel = new CartItemViewModel
            {
                CartItems = cartItems,
                GrandTotal = subtotal,
                ShippingPrice = shippingPrice,
                CouponCode = couponCode,
                DiscountAmount = discountAmount
            };

            var checkoutViewModel = new CheckoutViewModel
            {
                CartSummary = cartViewModel,
                Order = new OrderModel()
            };

            return View(checkoutViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CheckoutViewModel viewModel)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Repopulate CartSummary as it's not part of the form post
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            if (cartItems.Count == 0)
            {
                TempData["error"] = "Giỏ hàng của bạn đang trống!";
                return RedirectToAction("Index", "Cart");
            }

            // Recalculate totals on the server to ensure data integrity
            var shippingPriceCookie = Request.Cookies["ShippingPrice"];
            decimal shippingPrice = (shippingPriceCookie != null) ? JsonConvert.DeserializeObject<decimal>(shippingPriceCookie) : 0;

            var couponCode = Request.Cookies["CouponTitle"];
            decimal discountAmount = 0m;
            decimal subtotal = cartItems.Sum(i => i.Price * i.Quantity);

            if (!string.IsNullOrWhiteSpace(couponCode))
            {
                var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Name == couponCode && c.Status == 1 && c.DateStart <= DateTime.Now && c.DateExpired >= DateTime.Now && c.Quantity > 0);
                if (coupon != null)
                {
                    if (coupon.IsPercent) { discountAmount = Math.Round(subtotal * (coupon.DiscountValue / 100m), 2); }
                    else { discountAmount = coupon.DiscountValue; }
                    if (discountAmount > subtotal) discountAmount = subtotal;

                    coupon.Quantity -= 1;
                    _context.Update(coupon);
                }
            }

            var order = viewModel.Order;
            order.UserName = userEmail;
            order.OrderCode = Guid.NewGuid().ToString();
            order.Subtotal = subtotal;
            order.ShippingCost = shippingPrice;
            order.DiscountAmount = discountAmount;
            order.CouponCode = couponCode;
            order.Total = subtotal - discountAmount + shippingPrice;
            order.CreateDate = DateTime.Now;
            order.Status = 1; // 1 = Pending

            _context.Add(order);
            await _context.SaveChangesAsync();

            foreach (var cartItem in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    OrderCode = order.OrderCode,
                    UserName = userEmail,
                    ProductId = (int)cartItem.ProductId,
                    Price = cartItem.Price,
                    Quantity = cartItem.Quantity
                };
                _context.Add(orderDetail);

                var product = await _context.Products.FindAsync(cartItem.ProductId);
                if (product != null)
                {
                    product.Quantity -= cartItem.Quantity;
                    product.SoldOut += cartItem.Quantity;
                    _context.Update(product);
                }
            }

            await _context.SaveChangesAsync();

            HttpContext.Session.Remove("Cart");
            Response.Cookies.Delete("ShippingPrice");
            Response.Cookies.Delete("CouponTitle");

            var receiver = userEmail;
            var subject = "Xác nhận đơn hàng #" + order.OrderCode;
            var message = $"Cảm ơn {order.Name} đã đặt hàng! Chúng tôi đã nhận được đơn hàng của bạn và sẽ xử lý sớm nhất có thể. Tổng giá trị đơn hàng của bạn là {order.Total:N0} VNĐ.";
            await _emailSender.SendEmailAsync(receiver, subject, message);

            TempData["success"] = "Đặt hàng thành công!";
            return RedirectToAction("History", "Account");
        }
    }
}
