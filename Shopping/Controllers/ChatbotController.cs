using Microsoft.AspNetCore.Mvc;
using Shopping.Services;
using System.Threading.Tasks;

namespace Shopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatbotController : ControllerBase
    {
        private readonly GroqService _groqService;

        public ChatbotController(GroqService groqService)
        {
            _groqService = groqService;
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
                return Ok(new { response = response });
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
    }

    public class ChatRequest
    {
        public string Message { get; set; } = string.Empty;
    }
}
