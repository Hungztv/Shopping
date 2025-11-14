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
                    $"https://generativelanguage.googleapis.com/v1/models/gemini-1.5-flash:generateContent?key={_apiKey}",
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
                    return $"Xin lỗi, có lỗi kết nối API (Status: {response.StatusCode}). Vui lòng kiểm tra API key hoặc thử lại sau!";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetChatResponseAsync: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return $"Xin lỗi, có lỗi xảy ra: {ex.Message}. Vui lòng thử lại sau!";
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
