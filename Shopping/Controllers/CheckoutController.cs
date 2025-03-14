﻿using Microsoft.AspNetCore.Mvc;
using Shopping.Models;
using Shopping.Models.Repository;
using System.Security.Claims;

namespace Shopping.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly DataContext _context;
        public CheckoutController(DataContext context)
        {
            _context = context;
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
                    var orderDetail = new OrderDetails();
                    orderDetail.UserName = userEmail.Value;
                    orderDetail.OrderCode = ordercode;
                    orderDetail.Quantity = cart.Quantity;
                    orderDetail.Price = cart.Price;
                    orderDetail.ProductId = cart.ProductId;
                    _context.Add(orderDetail);
                    await _context.SaveChangesAsync();

                }
                HttpContext.Session.Remove("Cart");
                TempData["success"] = "Order Thành Công!";
                return RedirectToAction("Index");

            }
                return View();
        }
    }
}
