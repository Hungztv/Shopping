using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Models.Repository;
using Shopping.Models;


namespace Shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Coupon")]
    [Authorize(Roles = "Admin")]
    public class CouponController : Controller
    {
        private readonly DataContext _dataContext;
        public CouponController(DataContext context)
        {
            _dataContext = context;
        }
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var coupon_list = await _dataContext.Coupons.ToListAsync();
            ViewBag.Coupons = coupon_list;
            return View();
        }
        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CouponModel coupon)
        {


            if (ModelState.IsValid)
            {
                // Basic server-side guard for percent coupons
                if (coupon.IsPercent && (coupon.DiscountValue < 0 || coupon.DiscountValue > 100))
                {
                    ModelState.AddModelError("DiscountValue", "Phần trăm giảm phải nằm trong khoảng 0 - 100");
                    TempData["error"] = "Phần trăm giảm không hợp lệ";
                    return RedirectToAction("Index");
                }
                _dataContext.Add(coupon);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Thêm coupon thành công";
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
            return View();
        }

        [HttpPost]
        [Route("Apply")]
        public async Task<IActionResult> Apply(string code, decimal subtotal)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return Json(new { success = false, message = "Mã giảm giá trống" });
            }
            var coupon = await _dataContext.Coupons.FirstOrDefaultAsync(c => c.Name == code && c.Status == 1 && c.DateStart <= DateTime.Now && c.DateExpired >= DateTime.Now && c.Quantity > 0);
            if (coupon == null)
            {
                return Json(new { success = false, message = "Mã giảm giá không hợp lệ hoặc đã hết hạn" });
            }
            decimal discount = 0m;
            if (coupon.IsPercent)
            {
                discount = Math.Round(subtotal * (coupon.DiscountValue / 100m), 2);
            }
            else
            {
                discount = coupon.DiscountValue;
            }
            if (discount > subtotal) discount = subtotal;
            // (Optional) decrease quantity – reserve usage
            coupon.Quantity -= 1;
            _dataContext.Update(coupon);
            await _dataContext.SaveChangesAsync();
            var totalAfter = subtotal - discount;
            return Json(new { success = true, code = coupon.Name, discount, totalAfter });
        }
    }
}

