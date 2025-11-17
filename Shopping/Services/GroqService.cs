using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Shopping.Models;
using Shopping.Models.Repository;
using System.Text.RegularExpressions;

namespace Shopping.Services
{
    public class GroqService
    {
        private readonly string _apiKey;
        private readonly string _model;
        private readonly HttpClient _httpClient;
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public GroqService(IConfiguration configuration, HttpClient httpClient, DataContext context)
        {
            _configuration = configuration;
            _apiKey = configuration["Groq:ApiKey"] ?? Environment.GetEnvironmentVariable("GROQ_API_KEY") ?? string.Empty;
            _model = configuration["Groq:Model"] ?? "llama-3.3-70b-versatile";
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<string> GetChatResponseAsync(string userMessage)
        {
            try
            {
                var lower = userMessage.ToLowerInvariant();

                // 1) Parse intent and budget
                var (intentLaptop, intentSmartphone, maxBudget) = ParseIntentAndBudget(lower);

                // 2) Build query grounded to DB with intent + price filter
                IQueryable<ProductModel> baseQuery = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Brand);

                if (intentLaptop)
                {
                    baseQuery = baseQuery.Where(p =>
                        p.Category.Name.ToLower().Contains("laptop") ||
                        p.Name.ToLower().Contains("laptop") ||
                        p.Name.ToLower().Contains("notebook") ||
                        p.Name.ToLower().Contains("macbook") ||
                        p.Category.Name.ToLower().Contains("máy tính xách tay"));
                }
                else if (intentSmartphone)
                {
                    baseQuery = baseQuery.Where(p =>
                        p.Category.Name.ToLower().Contains("smartphone") ||
                        p.Category.Name.ToLower().Contains("điện thoại") ||
                        p.Name.ToLower().Contains("phone") ||
                        p.Name.ToLower().Contains("iphone") ||
                        p.Name.ToLower().Contains("galaxy") ||
                        p.Name.ToLower().Contains("xiaomi") ||
                        p.Name.ToLower().Contains("oppo") ||
                        p.Name.ToLower().Contains("vivo") ||
                        p.Name.ToLower().Contains("realme"));
                }
                else
                {
                    baseQuery = baseQuery.Where(p =>
                        p.Name.ToLower().Contains(lower) ||
                        p.Description.ToLower().Contains(lower) ||
                        p.Category.Name.ToLower().Contains(lower) ||
                        p.Brand.Name.ToLower().Contains(lower));
                }

                var matched = baseQuery
                    .Select(p => new { p.Name, p.Price, CategoryName = p.Category.Name, BrandName = p.Brand.Name })
                    .Take(50)
                    .ToList();

                // 3) Apply client-side budget filtering because Price is stored as string
                if (maxBudget > 0)
                {
                    matched = matched
                        .Where(p => TryParseVnd(p.Price, out var priceVnd) && priceVnd <= maxBudget)
                        .ToList();
                }

                // Order by numeric price descending for better ranking
                matched = matched
                    .OrderByDescending(p => TryParseVnd(p.Price, out var priceVnd) ? priceVnd : 0)
                    .Take(10)
                    .ToList();

                // Nếu không khớp, fallback top sản phẩm gần đây
                if (matched.Count == 0)
                {
                    matched = _context.Products
                        .Include(p => p.Category)
                        .Include(p => p.Brand)
                    .OrderByDescending(p => p.Id)
                        .Select(p => new { p.Name, p.Price, CategoryName = p.Category.Name, BrandName = p.Brand.Name })
                        .Take(10)
                        .ToList();
                }

                var productsContext = string.Join("\n", matched.Select(p =>
                {
                    var priceText = TryParseVnd(p.Price, out var v) ? v.ToString("N0") + " VNĐ" : p.Price;
                    return $"- {p.Name} ({p.CategoryName}/{p.BrandName}) — {priceText}";
                }));

                var systemPrompt = $@"Bạn là trợ lý mua sắm AI cho website Shopping.
Chỉ tư vấn dựa trên danh sách sản phẩm dưới đây. Nếu người dùng hỏi thứ không có trong danh sách, hãy nói không có dữ liệu và gợi ý danh mục liên quan.
Trả lời ngắn gọn, gợi ý 2-3 lựa chọn kèm giá (VNĐ) và điểm mạnh.
{(maxBudget > 0 ? $"Ngân sách tối đa: {maxBudget:N0} VNĐ\n" : string.Empty)}

Sản phẩm khả dụng:
{productsContext}";

                var groqUrl = "https://api.groq.com/openai/v1/chat/completions";

                using var request = new HttpRequestMessage(HttpMethod.Post, groqUrl);
                request.Headers.Add("Authorization", $"Bearer {_apiKey}");
                request.Headers.Add("Accept", "application/json");

                var payload = new
                {
                    model = _model,
                    messages = new object[]
                    {
                        new { role = "system", content = systemPrompt },
                        new { role = "user", content = userMessage }
                    },
                    temperature = 0.7,
                    top_p = 0.9,
                    max_tokens = 300
                };

                var json = JsonSerializer.Serialize(payload);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                Console.WriteLine($"Calling Groq API model '{_model}'...");
                var response = await _httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Groq Status: {response.StatusCode}");
                Console.WriteLine($"Groq Body: {responseContent.Substring(0, Math.Min(500, responseContent.Length))}");

                if (response.IsSuccessStatusCode)
                {
                    var parsed = JsonSerializer.Deserialize<GroqChatCompletionResponse>(responseContent);
                    var content = parsed?.choices?.FirstOrDefault()?.message?.content;
                    if (!string.IsNullOrWhiteSpace(content)) return content!;
                    return "Xin lỗi, mình chưa có đủ thông tin để trả lời câu hỏi này.";
                }
                else
                {
                    // Fallback: mock câu trả lời dựa trên danh sách sản phẩm
                    return GenerateMockResponse(userMessage, matched.Cast<dynamic>().ToList());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GroqService error: {ex.Message}");
                return "Xin lỗi, hệ thống AI đang bận. Bạn có thể thử lại sau hoặc mô tả rõ nhu cầu (ngân sách, thương hiệu, kích cỡ...).";
            }
        }

        private (bool intentLaptop, bool intentSmartphone, decimal maxBudget) ParseIntentAndBudget(string lower)
        {
            bool intentLaptop = lower.Contains("laptop") || lower.Contains("notebook") || lower.Contains("macbook") || lower.Contains("máy tính xách tay");
            bool intentSmartphone = lower.Contains("smartphone") || lower.Contains("điện thoại") || lower.Contains("phone") ||
                                   lower.Contains("iphone") || lower.Contains("samsung") || lower.Contains("xiaomi") ||
                                   lower.Contains("oppo") || lower.Contains("vivo") || lower.Contains("realme") ||
                                   lower.Contains("di động") || lower.Contains("di dong");

            // Extract numbers + unit (triệu, đ, vnd, million, k, nghìn)
            // Examples: "20 triệu", "15tr", "20000000", "18.5 triệu", "18,5 triệu"
            var regex = new Regex(@"(\d+[\.,]?\d*)\s*(triệu|tr|trieu|vnđ|đ|vnd|million|k|nghìn|nghin)?", RegexOptions.IgnoreCase);
            var m = regex.Match(lower);
            decimal maxBudget = 0;
            if (m.Success)
            {
                var numStr = m.Groups[1].Value.Replace(".", ",");
                if (decimal.TryParse(numStr, out var num))
                {
                    var unit = m.Groups[2].Value.ToLower();
                    if (unit == "triệu" || unit == "tr" || unit == "trieu" || unit == "million")
                    {
                        maxBudget = num * 1_000_000m;
                    }
                    else if (unit == "k" || unit == "nghìn" || unit == "nghin")
                    {
                        maxBudget = num * 1_000m;
                    }
                    else
                    {
                        // Assume raw VNĐ
                        maxBudget = num;
                    }

                    // If text includes "dưới"/"under"/"<=", we keep it as hard max. Otherwise treat as approx max (no change needed here)
                }
            }

            // If user says "dưới" or "under" with no unit but has a plain number like 20, assume triệu
            if (maxBudget == 0 && (lower.Contains("dưới") || lower.Contains("under") || lower.Contains("<") || lower.Contains("<=")))
            {
                var numOnly = new Regex(@"\b(\d{1,3})(?:[\.,](\d{3}))?\b").Match(lower);
                if (numOnly.Success && decimal.TryParse(numOnly.Groups[1].Value, out var n))
                {
                    maxBudget = n * 1_000_000m;
                }
            }

            return (intentLaptop, intentSmartphone, maxBudget);
        }

        private bool TryParseVnd(string priceText, out decimal value)
        {
            // Accept formats like "19.990.000", "19990000", "19,990,000", or with currency suffix
            var digits = new string(priceText.Where(char.IsDigit).ToArray());
            if (decimal.TryParse(digits, out var v))
            {
                value = v;
                return true;
            }
            value = 0;
            return false;
        }

        private string GenerateMockResponse(string userMessage, List<dynamic> products)
        {
            var lower = userMessage.ToLowerInvariant();
            var matched = products.Where(p =>
                    lower.Contains(p.Name.ToString().ToLower()) ||
                    lower.Contains(p.CategoryName.ToString().ToLower()) ||
                    lower.Contains(p.BrandName.ToString().ToLower()))
                .Take(3)
                .ToList();

            if (matched.Any())
            {
                var sb = new StringBuilder();
                sb.AppendLine("Gợi ý phù hợp với yêu cầu của bạn:\n");
                foreach (var p in matched)
                {
                    sb.AppendLine($"• {p.Name} ({p.BrandName}) — {p.Price:N0} VNĐ");
                }
                sb.AppendLine("\nBạn muốn so sánh chi tiết hay xem hình ảnh không?");
                return sb.ToString();
            }
            else
            {
                var top = products.Take(3).ToList();
                var sb = new StringBuilder();
                sb.AppendLine("Một vài lựa chọn tiêu biểu:\n");
                foreach (var p in top)
                {
                    sb.AppendLine($"• {p.Name} — {p.Price:N0} VNĐ");
                }
                sb.AppendLine("\nBạn có ngân sách / thương hiệu ưa thích không?");
                return sb.ToString();
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
            catch
            {
                return new List<ProductModel>();
            }
        }
    }

    public class GroqChatCompletionResponse
    {
        public List<GroqChoice>? choices { get; set; }
    }

    public class GroqChoice
    {
        public GroqMessage? message { get; set; }
    }

    public class GroqMessage
    {
        public string? role { get; set; }
        public string? content { get; set; }
    }
}
