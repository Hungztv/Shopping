using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shopping.Models;
using Shopping.Models.Momo;
using Shopping.Models.Repository;
using Shopping.Services;
using Shopping.Services.Momo;
using Shopping_Tutorial.Areas.Admin.Repository;
using DotNetEnv;

// Load environment variables from .env file in project root
var envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
if (File.Exists(envPath))
{
    Env.Load(envPath);
    Console.WriteLine($"✅ Loaded .env from: {envPath}");
}
else
{
    // Try parent directory
    envPath = Path.Combine(Directory.GetCurrentDirectory(), "..", ".env");
    if (File.Exists(envPath))
    {
        Env.Load(envPath);
        Console.WriteLine($"✅ Loaded .env from: {envPath}");
    }
    else
    {
        Console.WriteLine("⚠️ Warning: .env file not found. Using appsettings.json values.");
    }
}

var builder = WebApplication.CreateBuilder(args);

// Override configuration with environment variables
builder.Configuration["Groq:ApiKey"] = Environment.GetEnvironmentVariable("GROQ_API_KEY") ?? builder.Configuration["Groq:ApiKey"];
builder.Configuration["Groq:Model"] = Environment.GetEnvironmentVariable("GROQ_MODEL") ?? builder.Configuration["Groq:Model"];
builder.Configuration["GoogleKeys:ClientId"] = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID") ?? builder.Configuration["GoogleKeys:ClientId"];
builder.Configuration["GoogleKeys:ClientSecret"] = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET") ?? builder.Configuration["GoogleKeys:ClientSecret"];
builder.Configuration["MomoAPI:PartnerCode"] = Environment.GetEnvironmentVariable("MOMO_PARTNER_CODE") ?? builder.Configuration["MomoAPI:PartnerCode"];
builder.Configuration["MomoAPI:AccessKey"] = Environment.GetEnvironmentVariable("MOMO_ACCESS_KEY") ?? builder.Configuration["MomoAPI:AccessKey"];
builder.Configuration["MomoAPI:SecretKey"] = Environment.GetEnvironmentVariable("MOMO_SECRET_KEY") ?? builder.Configuration["MomoAPI:SecretKey"];
builder.Configuration["MomoAPI:MomoApiUrl"] = Environment.GetEnvironmentVariable("MOMO_API_URL") ?? builder.Configuration["MomoAPI:MomoApiUrl"];
builder.Configuration["MomoAPI:ReturnUrl"] = Environment.GetEnvironmentVariable("MOMO_RETURN_URL") ?? builder.Configuration["MomoAPI:ReturnUrl"];
builder.Configuration["MomoAPI:NotifyUrl"] = Environment.GetEnvironmentVariable("MOMO_NOTIFY_URL") ?? builder.Configuration["MomoAPI:NotifyUrl"];
builder.Configuration["MomoAPI:RequestType"] = Environment.GetEnvironmentVariable("MOMO_REQUEST_TYPE") ?? builder.Configuration["MomoAPI:RequestType"];

// Build connection string from environment variables
var dbServer = Environment.GetEnvironmentVariable("DB_SERVER");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbIntegratedSecurity = Environment.GetEnvironmentVariable("DB_INTEGRATED_SECURITY");
var dbEncrypt = Environment.GetEnvironmentVariable("DB_ENCRYPT");
var dbTrustServerCertificate = Environment.GetEnvironmentVariable("DB_TRUST_SERVER_CERTIFICATE");

if (!string.IsNullOrEmpty(dbServer) && !string.IsNullOrEmpty(dbName))
{
    builder.Configuration["ConnectionStrings:DbConnectedDb"] =
        $"Data Source={dbServer};Initial Catalog={dbName};Integrated Security={dbIntegratedSecurity};Encrypt={dbEncrypt};Trust Server Certificate={dbTrustServerCertificate}";
}

builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddScoped<IMomoService, MomoService>();

// Register AI services
builder.Services.AddHttpClient<GeminiService>();
builder.Services.AddScoped<GeminiService>();
builder.Services.AddHttpClient<GroqService>();
builder.Services.AddScoped<GroqService>();

builder.Services.AddControllersWithViews();
// Kết nối đến cơ sở dữ liệu
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DbConnectedDb"]));
// Thêm các dịch vụ vào container
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(30);
    options.Cookie.IsEssential = true;
});

builder.Services.AddIdentity<AppUserModel, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Cài đặt mật khẩu
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;

    options.User.RequireUniqueEmail = true;
});
//Login Google
builder.Services.AddAuthentication(options =>
{
    //options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie().AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientId").Value;
    options.ClientSecret = builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;
}); // --------LoginGG-----------
var app = builder.Build();

app.UseStatusCodePagesWithRedirects("/Home/Error?statuscode={0}");
app.UseSession();

// Cấu hình pipeline yêu cầu HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "category",
    pattern: "/category/{Slug?}",
    defaults: new { controller = "Category", action = "Index" });

app.MapControllerRoute(
    name: "brand",
    pattern: "/brand/{Slug?}",
    defaults: new { controller = "Brand", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 🛠 **Seed dữ liệu**
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DataContext>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    // Gọi hàm SeedData để thêm dữ liệu mặc định
    SeedData.SeedingData(context);

    // Gọi hàm tạo Role
    await InitializeRoles(services);

    // Tạo Admin Account
    await SeedData.CreateAdminAccount(services);
}
static async Task InitializeRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = { "Admin", "User", "Moderator" };

    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}

app.Run();
