using Shopping_Tutorial.Areas.Admin.Repository;
using Microsoft.AspNetCore.Mvc;
using Shopping.Models;
using Shopping.Models.Repository;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Shopping.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly DataContext _context;
        private readonly IEmailSender _emailSender;
        public CheckoutController(DataContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }
        public async Task<IActionResult> Checkout()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email);
            if (userEmail == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var ordercode = Guid.NewGuid().ToString();
                var orderItem = new OrderModel();
                orderItem.OrderCode = ordercode;
                orderItem.UserName = userEmail.Value;
                orderItem.CreateDate = DateTime.Now;
                orderItem.Status = 1;
                _context.Add(orderItem);
                await _context.SaveChangesAsync();
                List<CartItemModel> cartItem = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
                foreach (var cart in cartItem)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.UserName = userEmail.Value;
                    orderDetail.OrderCode = ordercode;
                    orderDetail.Quantity = cart.Quantity;
                    orderDetail.Price = cart.Price;
                    orderDetail.ProductId = (int)cart.ProductId;
                    //update product quantity
                    var product = await _context.Products.Where(p => p.Id == cart.ProductId).FirstAsync();
                    product.Quantity -= cart.Quantity;
                    product.SoldOut += cart.Quantity;
                    _context.Update(product);
                    _context.Add(orderDetail);
                    await _context.SaveChangesAsync();

                }
                HttpContext.Session.Remove("Cart");
                // Send Email
                var receiver = userEmail.Value;
                var subject = "Xác nhận đơn hàng";
                var message = "Cảm ơn bạn đã đặt hàng! Chúng tôi sẽ xử lý và giao hàng sớm nhất có thể.";

                await _emailSender.SendEmailAsync(receiver, subject, message);


                TempData["success"] = "Order Thành Công!";
                return RedirectToAction("Index");

            }
                return View();
        }
    }
}
