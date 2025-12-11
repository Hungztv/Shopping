using Microsoft.AspNetCore.Mvc;
using Shopping.Services;
using System.Threading.Tasks;
using Shopping.Models.Repository;
using System.Text.RegularExpressions;

namespace Shopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatbotController : ControllerBase
    {
        private readonly GroqService _groqService;
        private readonly DataContext _context;

        public ChatbotController(GroqService groqService, DataContext context)
        {
            _groqService = groqService;
            _context = context;
        }

        [HttpPost("message")]
        public async Task<IActionResult> SendMessage([FromBody] ChatRequest request)
        {
            if (string.IsNullOrEmpty(request.Message))
            {
                return BadRequest(new { error = "Tin nhắn không được để trống" });
            }

            try
            {
                var response = await _groqService.GetChatResponseAsync(request.Message);
                var baseUrl = $"{Request.Scheme}://{Request.Host}";
                response = Regex.Replace(response, @"/Product/Details/(\d+)", m => $"{baseUrl}/Product/Details/{m.Groups[1].Value}");
                return Ok(new { response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest(new { error = "Từ khóa tìm kiếm không được để trống" });
            }

            try
            {
                var products = await _groqService.SearchProductsAsync(query);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // Batch fetch product summaries for chatbot rendering
        [HttpGet("products")]
        public IActionResult GetProducts([FromQuery] string ids)
        {
            if (string.IsNullOrWhiteSpace(ids))
            {
                return BadRequest(new { error = "Thiếu danh sách ids" });
            }
            try
            {
                var idList = ids.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                 .Select(s => int.TryParse(s, out var v) ? v : -1)
                                 .Where(v => v > 0)
                                 .Distinct()
                                 .ToList();
                if (!idList.Any())
                {
                    return BadRequest(new { error = "Danh sách ids không hợp lệ" });
                }
                var baseUrl = $"{Request.Scheme}://{Request.Host}";
                var products = _context.Products
                    .Where(p => idList.Contains(p.Id))
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        p.Price,
                        p.Image,
                        Link = $"{baseUrl}/Product/Details/{p.Id}"
                    })
                    .ToList();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Có lỗi xảy ra: " + ex.Message });
            }
        }
    }

    public class ChatRequest
    {
        public string Message { get; set; } = string.Empty;
    }
}
