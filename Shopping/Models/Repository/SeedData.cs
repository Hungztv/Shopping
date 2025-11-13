using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Shopping.Models.Repository
{
    public class SeedData
    {
        public static void SeedingData(DataContext _context)
        {
            _context.Database.Migrate();
            if (!_context.Products.Any())
            {
                CategoryModel Macbook = new CategoryModel { Name = "Macbook", Slug = "Macbook", Description = "Macbook is the new", Status = "Active" };
                CategoryModel pc = new CategoryModel { Name = "PC", Slug = "PC", Description = "PC is the new", Status = "Active" };
                BrandModel apple = new BrandModel { Name = "Apple", Slug = "Apple", Description = "Apple is the new", Status = "Active" };
                BrandModel samsung = new BrandModel { Name = "Samsung", Slug = "Samsung", Description = "Samsung is the new", Status = "Active" };
                _context.Products.AddRange(
                    new ProductModel
                    {

                        Name = "iPhone 16",
                        Slug = "IPhone16",
                        Description = "Iphone 16 is the new",
                        Category = Macbook,
                        Brand = apple,
                        Image = "1.jpg",
                        Price = "20.000.000 VNĐ",
                        CapitalPrice = "18.000.000 VNĐ",
                        Quantity = 100,
                        SoldOut = 0
                    },
                    new ProductModel
                    {

                        Name = "Samsung Galaxy",
                        Slug = "SamsungGalaxy",
                        Description = "Samsung Galaxy is the new",
                        Category = pc,
                        Brand = samsung,
                        Image = "2.jpg",
                        Price = "20.000.000 VNĐ",
                        CapitalPrice = "18.000.000 VNĐ",
                        Quantity = 100,
                        SoldOut = 0
                    }
                );
                _context.SaveChanges();
            }
        }

        // Tạo Admin Account
        public static async Task CreateAdminAccount(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUserModel>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Đảm bảo role Admin đã tồn tại
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Kiểm tra xem admin đã tồn tại chưa
            var adminEmail = "admin@shopping.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                // Tạo admin user
                var admin = new AppUserModel
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    PhoneNumber = "0865322936",
                    occupation = "Administrator"
                };

                var result = await userManager.CreateAsync(admin, "Admin@123"); // Mật khẩu mặc định

                if (result.Succeeded)
                {
                    // Thêm vào role Admin
                    await userManager.AddToRoleAsync(admin, "Admin");
                    Console.WriteLine("Admin account created successfully!");
                    Console.WriteLine($"Email: {adminEmail}");
                    Console.WriteLine("Password: Admin@123");
                }
                else
                {
                    Console.WriteLine("Failed to create admin account:");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"- {error.Description}");
                    }
                }
            }
        }

    }
}
