using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Shopping.Models;
using Shopping.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace Shopping.Services
{
    public class GeminiService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public GeminiService(IConfiguration configuration, HttpClient httpClient, DataContext context)
        {
            _configuration = configuration;
            _apiKey = configuration["Gemini:ApiKey"] ?? "";
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<string> GetChatResponseAsync(string userMessage)
        {
            try
            {
                // Lấy context về sản phẩm từ database
                var products = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Brand)
                    .Select(p => new { p.Name, p.Price, CategoryName = p.Category.Name, BrandName = p.Brand.Name, p.Description })
                    .Take(30)
                    .ToList();

                var productsContext = string.Join("\n", products.Select(p =>
                    $"- {p.Name} ({p.CategoryName} - {p.BrandName}): {p.Price:N0} VNĐ"));

                // Tạo system prompt với context (ngắn gọn hơn)
                var systemPrompt = $@"Bạn là trợ lý mua sắm AI. Tư vấn sản phẩm dựa trên danh sách dưới đây:

{productsContext}

Khách hỏi: {userMessage}

Trả lời ngắn gọn, gợi ý 2-3 sản phẩm phù hợp với giá.";

                // Gọi Gemini API
                var requestBody = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = systemPrompt }
                            }
                        }
                    },
                    generationConfig = new
                    {
                        temperature = 0.7,
                        maxOutputTokens = 300,
                        topP = 0.95,
                        topK = 40
                    }
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                Console.WriteLine($"Calling Gemini API with key: {_apiKey?.Substring(0, 10)}...");

                var response = await _httpClient.PostAsync(
                    $"https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key={_apiKey}",
                    content
                );

                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Response Status: {response.StatusCode}");
                Console.WriteLine($"API Response: {responseContent.Substring(0, Math.Min(500, responseContent.Length))}");

                if (response.IsSuccessStatusCode)
                {
                    var geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(responseContent);

                    var botMessage = geminiResponse?.candidates?[0]?.content?.parts?[0]?.text ??
                        "Xin lỗi, tôi không thể trả lời câu hỏi này. Vui lòng thử lại!";

                    return botMessage;
                }
                else
                {
                    Console.WriteLine($"Gemini API Error: {response.StatusCode} - {responseContent}");

                    // Mock response khi API lỗi
                    var mockResponse = GenerateMockResponse(userMessage, products.Cast<dynamic>().ToList());
                    return mockResponse;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetChatResponseAsync: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return $"Xin chào! Tôi có thể giúp bạn tìm kiếm sản phẩm. Hiện tại hệ thống AI đang bảo trì, vui lòng liên hệ hotline để được tư vấn chi tiết hơn! 📞";
            }
        }

        private string GenerateMockResponse(string userMessage, List<dynamic> products)
        {
            var lowerMessage = userMessage.ToLower();

            // Tìm sản phẩm phù hợp
            var matchedProducts = products.Where(p =>
                lowerMessage.Contains(p.Name.ToString().ToLower()) ||
                lowerMessage.Contains(p.CategoryName.ToString().ToLower()) ||
                lowerMessage.Contains(p.BrandName.ToString().ToLower())
            ).Take(3).ToList();

            if (matchedProducts.Any())
            {
                var response = $"Dựa trên yêu cầu của bạn, tôi gợi ý các sản phẩm sau:\n\n";
                foreach (var product in matchedProducts)
                {
                    response += $"🔹 **{product.Name}** - {product.BrandName}\n";
                    response += $"   💰 Giá: {product.Price:N0} VNĐ\n\n";
                }
                response += "Bạn có muốn xem thêm thông tin về sản phẩm nào không? 😊";
                return response;
            }
            else
            {
                // Gợi ý sản phẩm phổ biến
                var popularProducts = products.Take(3).ToList();
                var response = "Chúng tôi có nhiều sản phẩm chất lượng:\n\n";
                foreach (var product in popularProducts)
                {
                    response += $"🔹 **{product.Name}**\n";
                    response += $"   💰 {product.Price:N0} VNĐ\n\n";
                }
                response += "Bạn đang tìm loại sản phẩm nào? Hãy cho tôi biết để tư vấn chính xác hơn! 🛍️";
                return response;
            }
        }

        public async Task<List<ProductModel>> SearchProductsAsync(string query)
        {
            try
            {
                return _context.Products
                    .Where(p => p.Name.Contains(query) ||
                               p.Description.Contains(query) ||
                               p.Category.Name.Contains(query) ||
                               p.Brand.Name.Contains(query))
                    .Take(5)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchProductsAsync: {ex.Message}");
                return new List<ProductModel>();
            }
        }
    }

    // Response models for Gemini API
    public class GeminiResponse
    {
        public List<GeminiCandidate>? candidates { get; set; }
    }

    public class GeminiCandidate
    {
        public GeminiContent? content { get; set; }
    }

    public class GeminiContent
    {
        public List<GeminiPart>? parts { get; set; }
    }

    public class GeminiPart
    {
        public string? text { get; set; }
    }
}
