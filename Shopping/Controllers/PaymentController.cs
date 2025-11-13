using Microsoft.AspNetCore.Mvc;
using Shopping.Models;
using Shopping.Services.Momo;


namespace Shopping.Controllers
{

    public class PaymentController : Controller
    {

        private IMomoService _momoService;
        public PaymentController(IMomoService momoService)
        {
            _momoService = momoService;

        }
        [HttpPost]
        public async Task<IActionResult> CreatePaymentMomo(OrderInfo model)
        {
            // Validate input
            if (model == null || string.IsNullOrEmpty(model.FullName))
            {
                TempData["Error"] = "Thông tin đơn hàng không hợp lệ";
                return RedirectToAction("Index", "Checkout");
            }

            var response = await _momoService.CreatePaymentAsync(model);

            // Check if payment URL is returned
            if (response == null)
            {
                TempData["Error"] = "Không thể kết nối tới cổng thanh toán Momo";
                return RedirectToAction("Index", "Checkout");
            }

            // Check for Momo API errors
            if (response.ErrorCode != 0 || string.IsNullOrEmpty(response.PayUrl))
            {
                TempData["Error"] = $"Lỗi từ Momo: {response.Message ?? "Không thể tạo giao dịch"}";
                return RedirectToAction("Index", "Checkout");
            }

            return Redirect(response.PayUrl);
        }


        [HttpGet]
        public IActionResult PaymentCallBack()
        {
            var response = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
            return View(response);
        }




    }
}
