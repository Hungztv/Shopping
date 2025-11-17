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
                CategoryModel laptop = new CategoryModel { Name = "Laptop", Slug = "laptop", Description = "Laptop học tập và làm việc", Status = "Active" };
                CategoryModel smartphone = new CategoryModel { Name = "Smartphone", Slug = "smartphone", Description = "Điện thoại thông minh", Status = "Active" };

                BrandModel apple = new BrandModel { Name = "Apple", Slug = "Apple", Description = "Apple is the new", Status = "Active" };
                BrandModel samsung = new BrandModel { Name = "Samsung", Slug = "Samsung", Description = "Samsung is the new", Status = "Active" };
                BrandModel acer = new BrandModel { Name = "Acer", Slug = "acer", Description = "Acer laptops and peripherals", Status = "Active" };
                BrandModel asus = new BrandModel { Name = "ASUS", Slug = "asus", Description = "ASUS computers and devices", Status = "Active" };
                BrandModel dell = new BrandModel { Name = "Dell", Slug = "dell", Description = "Dell business and personal computers", Status = "Active" };
                BrandModel hp = new BrandModel { Name = "HP", Slug = "hp", Description = "HP laptops and printers", Status = "Active" };
                BrandModel lenovo = new BrandModel { Name = "Lenovo", Slug = "lenovo", Description = "Lenovo ThinkPad and IdeaPad series", Status = "Active" };
                BrandModel xiaomi = new BrandModel { Name = "Xiaomi", Slug = "xiaomi", Description = "Xiaomi smartphones and devices", Status = "Active" };
                BrandModel oppo = new BrandModel { Name = "OPPO", Slug = "oppo", Description = "OPPO smartphones", Status = "Active" };
                BrandModel vivo = new BrandModel { Name = "Vivo", Slug = "vivo", Description = "Vivo smartphones", Status = "Active" };
                BrandModel realme = new BrandModel { Name = "Realme", Slug = "realme", Description = "Realme smartphones", Status = "Active" };

                _context.Products.AddRange(
                    new ProductModel
                    {
                        Name = "iPhone 16",
                        Slug = "IPhone16",
                        Description = "Iphone 16 is the new",
                        Category = Macbook,
                        Brand = apple,
                        Image = "1.jpg",
                        Price = "20.000.000",
                        CapitalPrice = "18.000.000",
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
                        Price = "20.000.000",
                        CapitalPrice = "18.000.000",
                        Quantity = 100,
                        SoldOut = 0
                    },
                    // Laptop học tập dưới 20 triệu
                    new ProductModel
                    {
                        Name = "Acer Aspire 5 A515-58M",
                        Slug = "acer-aspire-5-a515-58m",
                        Description = "Laptop học tập, văn phòng. Intel Core i5-1335U, RAM 8GB, SSD 512GB, 15.6 inch FHD. Pin 8 giờ, thiết kế nhẹ.",
                        Category = laptop,
                        Brand = acer,
                        Image = "laptop-acer-aspire5.jpg",
                        Price = "15990000",
                        CapitalPrice = "14500000",
                        Quantity = 25,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "ASUS Vivobook 15 X1502ZA",
                        Slug = "asus-vivobook-15-x1502za",
                        Description = "Laptop đa năng cho học sinh, sinh viên. Core i5-1240P, RAM 8GB, SSD 512GB, màn hình 15.6 inch FHD. Bàn phím thoải mái.",
                        Category = laptop,
                        Brand = asus,
                        Image = "laptop-asus-vivobook15.jpg",
                        Price = "17490000",
                        CapitalPrice = "16200000",
                        Quantity = 20,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Dell Inspiron 14 5430",
                        Slug = "dell-inspiron-14-5430",
                        Description = "Laptop học tập cao cấp. Intel Core i5-1340P, RAM 16GB, SSD 512GB, màn hình 14 inch FHD+. Mỏng nhẹ chỉ 1.5kg.",
                        Category = laptop,
                        Brand = dell,
                        Image = "laptop-dell-inspiron14.jpg",
                        Price = "18990000",
                        CapitalPrice = "17500000",
                        Quantity = 18,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "HP 15s-fq5231TU",
                        Slug = "hp-15s-fq5231tu",
                        Description = "Laptop văn phòng giá tốt. Intel Core i3-1215U, RAM 8GB, SSD 256GB, 15.6 inch HD. Bền bỉ, pin lâu.",
                        Category = laptop,
                        Brand = hp,
                        Image = "laptop-hp-15s.jpg",
                        Price = "12490000",
                        CapitalPrice = "11200000",
                        Quantity = 30,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Lenovo IdeaPad Slim 3 15IAH8",
                        Slug = "lenovo-ideapad-slim3-15iah8",
                        Description = "Laptop học tập và giải trí. Intel Core i5-12450H, RAM 8GB, SSD 512GB, 15.6 inch FHD. Âm thanh Dolby Audio.",
                        Category = laptop,
                        Brand = lenovo,
                        Image = "laptop-lenovo-ideapad-slim3.jpg",
                        Price = "16790000",
                        CapitalPrice = "15400000",
                        Quantity = 22,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "ASUS TUF Gaming F15 FX507ZC4",
                        Slug = "asus-tuf-gaming-f15-fx507zc4",
                        Description = "Laptop gaming giá rẻ. Core i5-12500H, RTX 3050 4GB, RAM 8GB, SSD 512GB, 15.6 inch FHD 144Hz. Mạnh mẽ cho học tập và giải trí.",
                        Category = laptop,
                        Brand = asus,
                        Image = "laptop-asus-tuf-f15.jpg",
                        Price = "19990000",
                        CapitalPrice = "18800000",
                        Quantity = 15,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Acer Nitro 5 AN515-58",
                        Slug = "acer-nitro5-an515-58",
                        Description = "Laptop gaming phổ thông. Core i5-12450H, RTX 3050 4GB, RAM 8GB, SSD 512GB, 15.6 inch FHD 144Hz. Tản nhiệt tốt.",
                        Category = laptop,
                        Brand = acer,
                        Image = "laptop-acer-nitro5.jpg",
                        Price = "19490000",
                        CapitalPrice = "18200000",
                        Quantity = 12,
                        SoldOut = 0
                    },
                    // Smartphones giá tốt
                    new ProductModel
                    {
                        Name = "Xiaomi Redmi Note 13",
                        Slug = "xiaomi-redmi-note-13",
                        Description = "Smartphone giá rẻ. Snapdragon 685, RAM 6GB, ROM 128GB, màn hình AMOLED 6.67 inch 120Hz, camera 108MP, pin 5000mAh sạc nhanh 33W.",
                        Category = smartphone,
                        Brand = xiaomi,
                        Image = "phone-xiaomi-redmi-note13.jpg",
                        Price = "4990000",
                        CapitalPrice = "4500000",
                        Quantity = 40,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Samsung Galaxy A15 5G",
                        Slug = "samsung-galaxy-a15-5g",
                        Description = "Smartphone 5G giá tốt. MediaTek Dimensity 6100+, RAM 8GB, ROM 128GB, màn hình Super AMOLED 6.5 inch 90Hz, camera 50MP, pin 5000mAh.",
                        Category = smartphone,
                        Brand = samsung,
                        Image = "phone-samsung-a15.jpg",
                        Price = "5490000",
                        CapitalPrice = "5000000",
                        Quantity = 35,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "OPPO A78 5G",
                        Slug = "oppo-a78-5g",
                        Description = "Smartphone phổ thông. MediaTek Dimensity 700, RAM 8GB, ROM 128GB, màn hình IPS LCD 6.56 inch 90Hz, camera 50MP, pin 5000mAh sạc nhanh 33W.",
                        Category = smartphone,
                        Brand = oppo,
                        Image = "phone-oppo-a78.jpg",
                        Price = "5990000",
                        CapitalPrice = "5400000",
                        Quantity = 30,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Vivo Y36",
                        Slug = "vivo-y36",
                        Description = "Smartphone giải trí. Snapdragon 680, RAM 8GB, ROM 128GB, màn hình IPS LCD 6.64 inch 90Hz, camera 50MP, pin 5000mAh sạc nhanh 44W.",
                        Category = smartphone,
                        Brand = vivo,
                        Image = "phone-vivo-y36.jpg",
                        Price = "5290000",
                        CapitalPrice = "4800000",
                        Quantity = 32,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Realme C55",
                        Slug = "realme-c55",
                        Description = "Smartphone pin khủng. Helio G88, RAM 6GB, ROM 128GB, màn hình IPS LCD 6.72 inch 90Hz, camera 64MP, pin 5000mAh sạc nhanh 33W.",
                        Category = smartphone,
                        Brand = realme,
                        Image = "phone-realme-c55.jpg",
                        Price = "4490000",
                        CapitalPrice = "4100000",
                        Quantity = 45,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Samsung Galaxy A25 5G",
                        Slug = "samsung-galaxy-a25-5g",
                        Description = "Smartphone tầm trung. Exynos 1280, RAM 8GB, ROM 128GB, màn hình Super AMOLED 6.5 inch 120Hz, camera 50MP OIS, pin 5000mAh sạc nhanh 25W.",
                        Category = smartphone,
                        Brand = samsung,
                        Image = "phone-samsung-a25.jpg",
                        Price = "6990000",
                        CapitalPrice = "6400000",
                        Quantity = 28,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Xiaomi Redmi 13C",
                        Slug = "xiaomi-redmi-13c",
                        Description = "Smartphone giá rẻ nhất. MediaTek Helio G85, RAM 4GB, ROM 128GB, màn hình IPS LCD 6.74 inch 90Hz, camera 50MP, pin 5000mAh sạc 18W.",
                        Category = smartphone,
                        Brand = xiaomi,
                        Image = "phone-xiaomi-redmi-13c.jpg",
                        Price = "3290000",
                        CapitalPrice = "3000000",
                        Quantity = 50,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "OPPO A58 5G",
                        Slug = "oppo-a58-5g",
                        Description = "Smartphone 5G cơ bản. MediaTek Dimensity 700, RAM 6GB, ROM 128GB, màn hình IPS LCD 6.56 inch 90Hz, camera 50MP, pin 5000mAh sạc 33W.",
                        Category = smartphone,
                        Brand = oppo,
                        Image = "phone-oppo-a58.jpg",
                        Price = "4790000",
                        CapitalPrice = "4300000",
                        Quantity = 38,
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
