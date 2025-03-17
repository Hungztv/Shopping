using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Models.Repository;

namespace Shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Order")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly DataContext _dataContext;
        public OrderController(DataContext context)
        {
            _dataContext = context;
        }
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Orders.OrderByDescending(p => p.Id).ToListAsync());
        }
        [HttpGet]
        [Route("ViewOrder")]
        public async Task<IActionResult> ViewOrder(string ordercode)
        {
            var DetailsOrder = await _dataContext.OrderDetails
                .Include(od => od.Product)  // Load Product từ DB
                .Where(od => od.OrderCode == ordercode)
                .ToListAsync();

            var Order = await _dataContext.Orders
                .Where(o => o.OrderCode == ordercode)
                .FirstOrDefaultAsync();

            if (Order == null)
            {
                return NotFound(); // Trả về lỗi nếu không tìm thấy đơn hàng
            }

            // Debug: In ra Console để kiểm tra dữ liệu
            foreach (var item in DetailsOrder)
            {
                Console.WriteLine($"OrderCode: {item.OrderCode}, ProductId: {item.ProductId}, ProductName: {item.Product?.Name ?? "NULL"}");
            }

            ViewBag.Status = Order.Status;
            return View(DetailsOrder);
        }
        [HttpPost]
        [Route("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(string ordercode, int status)
        {
            if (string.IsNullOrEmpty(ordercode))
            {
                return BadRequest(new { success = false, message = "Mã đơn hàng không hợp lệ." });
            }

            var order = await _dataContext.Orders.FirstOrDefaultAsync(o => o.OrderCode == ordercode);

            if (order == null)
            {
                return NotFound(new { success = false, message = "Không tìm thấy đơn hàng." });
            }

            order.Status = status;
            _dataContext.Orders.Update(order); 

            try
            {
                await _dataContext.SaveChangesAsync();
                return Ok(new { success = true, message = "Cập nhật đơn hàng thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi khi cập nhật đơn hàng: " + ex.Message });
            }
        }
        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> Delete(string ordercode)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(o => o.OrderCode == ordercode);

            if (order == null)
            {
                return NotFound();
            }
            try
            {

                //delete order
                _dataContext.Orders.Remove(order);


                await _dataContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while deleting the order.");
            }
        }

    }
}