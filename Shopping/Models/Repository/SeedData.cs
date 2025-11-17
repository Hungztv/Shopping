using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shopping.Models;

namespace Shopping.Models.Repository
{
    public class SeedData
    {
        public static void SeedingData(DataContext _context)
        {
            _context.Database.Migrate();
            if (!_context.Products.Any())
            {
                CategoryModel macbook = new CategoryModel { Name = "Macbook", Description = "Apple Macbooks", Slug = "macbook", Status = "1" };
                CategoryModel pc = new CategoryModel { Name = "PC", Description = "Desktop computers", Slug = "pc", Status = "1" };
                CategoryModel laptop = new CategoryModel { Name = "Laptop", Description = "Portable laptops", Slug = "laptop", Status = "1" };
                CategoryModel smartphone = new CategoryModel { Name = "Smartphone", Description = "Mobile phones", Slug = "smartphone", Status = "1" };

                BrandModel apple = new BrandModel { Name = "Apple", Description = "Apple Inc.", Slug = "apple", Status = "1" };
                BrandModel samsung = new BrandModel { Name = "Samsung", Description = "Samsung Electronics", Slug = "samsung", Status = "1" };
                BrandModel acer = new BrandModel { Name = "Acer", Description = "Acer Inc.", Slug = "acer", Status = "1" };
                BrandModel asus = new BrandModel { Name = "ASUS", Description = "ASUSTeK Computer", Slug = "asus", Status = "1" };
                BrandModel dell = new BrandModel { Name = "Dell", Description = "Dell Technologies", Slug = "dell", Status = "1" };
                BrandModel hp = new BrandModel { Name = "HP", Description = "HP Inc.", Slug = "hp", Status = "1" };
                BrandModel lenovo = new BrandModel { Name = "Lenovo", Description = "Lenovo Group", Slug = "lenovo", Status = "1" };
                BrandModel xiaomi = new BrandModel { Name = "Xiaomi", Description = "Xiaomi Corporation", Slug = "xiaomi", Status = "1" };
                BrandModel oppo = new BrandModel { Name = "OPPO", Description = "OPPO Electronics", Slug = "oppo", Status = "1" };
                BrandModel vivo = new BrandModel { Name = "Vivo", Description = "Vivo Mobile Communication", Slug = "vivo", Status = "1" };
                BrandModel realme = new BrandModel { Name = "Realme", Description = "Realme Chongqing", Slug = "realme", Status = "1" };

                _context.Categories.AddRange(macbook, pc, laptop, smartphone);
                _context.Brands.AddRange(apple, samsung, acer, asus, dell, hp, lenovo, xiaomi, oppo, vivo, realme);
                _context.SaveChanges();

                _context.Products.AddRange(
                    // LAPTOPS (10 products)
                    new ProductModel
                    {
                        Name = "MacBook Air M2 13 inch 2024",
                        Slug = "macbook-air-m2-13-2024",
                        Description = "MacBook Air M2 chip, 8GB RAM, 256GB SSD, 13.6 inch Retina Display",
                        Image = "https://images.unsplash.com/photo-1517336714731-489689fd1ca8?w=800",
                        Category = macbook,
                        Brand = apple,
                        Price = 27990000,
                        CapitalPrice = 25500000,
                        Quantity = 50,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "MacBook Pro M3 14 inch 2024",
                        Slug = "macbook-pro-m3-14-2024",
                        Description = "MacBook Pro M3 chip, 16GB RAM, 512GB SSD, 14 inch Liquid Retina XDR Display",
                        Image = "https://images.unsplash.com/photo-1611186871348-b1ce696e52c9?w=800",
                        Category = macbook,
                        Brand = apple,
                        Price = 39990000,
                        CapitalPrice = 37000000,
                        Quantity = 30,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Laptop Acer Aspire 5 A515-58-53S7",
                        Slug = "acer-aspire-5-a515-58-53s7",
                        Description = "Intel Core i5-13420H, 8GB RAM, 512GB SSD, 15.6 inch FHD",
                        Image = "https://images.unsplash.com/photo-1588872657578-7efd1f1555ed?w=800",
                        Category = laptop,
                        Brand = acer,
                        Price = 15990000,
                        CapitalPrice = 14000000,
                        Quantity = 60,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Laptop ASUS Vivobook 15 X1504VA-NJ023W",
                        Slug = "asus-vivobook-15-x1504va-nj023w",
                        Description = "Intel Core i5-1335U, 16GB RAM, 512GB SSD, 15.6 inch FHD",
                        Image = "https://images.unsplash.com/photo-1593642632823-8f785ba67e45?w=800",
                        Category = laptop,
                        Brand = asus,
                        Price = 17490000,
                        CapitalPrice = 16000000,
                        Quantity = 45,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Laptop Dell Inspiron 15 3520",
                        Slug = "dell-inspiron-15-3520",
                        Description = "Intel Core i5-1235U, 8GB RAM, 512GB SSD, 15.6 inch FHD",
                        Image = "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=800",
                        Category = laptop,
                        Brand = dell,
                        Price = 18990000,
                        CapitalPrice = 17000000,
                        Quantity = 40,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Laptop HP 15s-fq5231TU",
                        Slug = "hp-15s-fq5231tu",
                        Description = "Intel Core i3-1215U, 8GB RAM, 256GB SSD, 15.6 inch FHD",
                        Image = "https://images.unsplash.com/photo-1587202372634-32705e3bf49c?w=800",
                        Category = laptop,
                        Brand = hp,
                        Price = 12490000,
                        CapitalPrice = 11000000,
                        Quantity = 55,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Laptop Lenovo IdeaPad Slim 3 15IAH8",
                        Slug = "lenovo-ideapad-slim-3-15iah8",
                        Description = "Intel Core i5-12450H, 16GB RAM, 512GB SSD, 15.6 inch FHD",
                        Image = "https://images.unsplash.com/photo-1603302576837-37561b2e2302?w=800",
                        Category = laptop,
                        Brand = lenovo,
                        Price = 16790000,
                        CapitalPrice = 15000000,
                        Quantity = 38,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Laptop Dell XPS 13 9340",
                        Slug = "dell-xps-13-9340",
                        Description = "Intel Core Ultra 7-155H, 16GB RAM, 512GB SSD, 13.4 inch FHD+",
                        Image = "https://images.unsplash.com/photo-1602080858428-57174f9431cf?w=800",
                        Category = laptop,
                        Brand = dell,
                        Price = 32990000,
                        CapitalPrice = 30000000,
                        Quantity = 25,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Laptop ASUS TUF Gaming F15 FX507ZC4-HN095W",
                        Slug = "asus-tuf-gaming-f15-fx507zc4-hn095w",
                        Description = "Intel Core i5-12500H, 8GB RAM, 512GB SSD, RTX 3050, 15.6 inch FHD 144Hz",
                        Image = "https://images.unsplash.com/photo-1618424181497-157f25b6ddd5?w=800",
                        Category = laptop,
                        Brand = asus,
                        Price = 19990000,
                        CapitalPrice = 18000000,
                        Quantity = 35,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Laptop Acer Nitro 5 Tiger AN515-58-52SP",
                        Slug = "acer-nitro-5-tiger-an515-58-52sp",
                        Description = "Intel Core i5-12450H, 8GB RAM, 512GB SSD, RTX 3050, 15.6 inch FHD 144Hz",
                        Image = "https://images.unsplash.com/photo-1625948515291-69613efd103f?w=800",
                        Category = laptop,
                        Brand = acer,
                        Price = 19490000,
                        CapitalPrice = 17500000,
                        Quantity = 42,
                        SoldOut = 0
                    },

                    // SMARTPHONES (11 products)
                    new ProductModel
                    {
                        Name = "iPhone 15 128GB",
                        Slug = "iphone-15-128gb",
                        Description = "A16 Bionic chip, 6.1 inch Super Retina XDR, 48MP Camera, 5G",
                        Image = "https://images.unsplash.com/photo-1695048133142-1a20484d2569?w=800",
                        Category = smartphone,
                        Brand = apple,
                        Price = 22990000,
                        CapitalPrice = 21000000,
                        Quantity = 70,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Samsung Galaxy S23 FE 5G 8GB/256GB",
                        Slug = "samsung-galaxy-s23-fe-5g",
                        Description = "Exynos 2200, 6.4 inch Dynamic AMOLED 2X, 50MP Camera, 5G",
                        Image = "https://images.unsplash.com/photo-1610945415295-d9bbf067e59c?w=800",
                        Category = smartphone,
                        Brand = samsung,
                        Price = 12990000,
                        CapitalPrice = 11500000,
                        Quantity = 80,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Samsung Galaxy A54 5G 8GB/256GB",
                        Slug = "samsung-galaxy-a54-5g",
                        Description = "Exynos 1380, 6.4 inch Super AMOLED, 50MP Camera, 5G",
                        Image = "https://images.unsplash.com/photo-1567581935884-3349723552ca?w=800",
                        Category = smartphone,
                        Brand = samsung,
                        Price = 10990000,
                        CapitalPrice = 10000000,
                        Quantity = 90,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Xiaomi 13T 5G 12GB/256GB",
                        Slug = "xiaomi-13t-5g",
                        Description = "MediaTek Dimensity 8200-Ultra, 6.67 inch AMOLED, 50MP Camera, 5G",
                        Image = "https://images.unsplash.com/photo-1598327105666-5b89351aff97?w=800",
                        Category = smartphone,
                        Brand = xiaomi,
                        Price = 11990000,
                        CapitalPrice = 10500000,
                        Quantity = 65,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Xiaomi Redmi Note 13 6GB/128GB",
                        Slug = "xiaomi-redmi-note-13",
                        Description = "Snapdragon 685, 6.67 inch AMOLED, 108MP Camera, 4G",
                        Image = "https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=800",
                        Category = smartphone,
                        Brand = xiaomi,
                        Price = 4990000,
                        CapitalPrice = 4200000,
                        Quantity = 120,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "OPPO Reno10 5G 8GB/256GB",
                        Slug = "oppo-reno10-5g",
                        Description = "MediaTek Dimensity 7050, 6.7 inch AMOLED, 64MP Camera, 5G",
                        Image = "https://images.unsplash.com/photo-1585060544812-6b45742d762f?w=800",
                        Category = smartphone,
                        Brand = oppo,
                        Price = 9990000,
                        CapitalPrice = 9000000,
                        Quantity = 75,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "OPPO A78 5G 8GB/128GB",
                        Slug = "oppo-a78-5g",
                        Description = "MediaTek Dimensity 700, 6.56 inch IPS LCD, 50MP Camera, 5G",
                        Image = "https://images.unsplash.com/photo-1592286927505-2fd0dc2c3caa?w=800",
                        Category = smartphone,
                        Brand = oppo,
                        Price = 5990000,
                        CapitalPrice = 5200000,
                        Quantity = 100,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Vivo V29e 5G 12GB/256GB",
                        Slug = "vivo-v29e-5g",
                        Description = "Snapdragon 695, 6.78 inch AMOLED, 64MP Camera, 5G",
                        Image = "https://images.unsplash.com/photo-1580910051074-3eb694886505?w=800",
                        Category = smartphone,
                        Brand = vivo,
                        Price = 8990000,
                        CapitalPrice = 8000000,
                        Quantity = 68,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Vivo Y36 4G 8GB/128GB",
                        Slug = "vivo-y36-4g",
                        Description = "Snapdragon 680, 6.64 inch IPS LCD, 50MP Camera, 4G",
                        Image = "https://images.unsplash.com/photo-1556656793-08538906a9f8?w=800",
                        Category = smartphone,
                        Brand = vivo,
                        Price = 5290000,
                        CapitalPrice = 4500000,
                        Quantity = 95,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Realme 11 Pro 5G 12GB/256GB",
                        Slug = "realme-11-pro-5g",
                        Description = "MediaTek Dimensity 7050, 6.7 inch AMOLED, 100MP Camera, 5G",
                        Image = "https://images.unsplash.com/photo-1574944985070-8f3ebc6b79d2?w=800",
                        Category = smartphone,
                        Brand = realme,
                        Price = 9490000,
                        CapitalPrice = 8500000,
                        Quantity = 72,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "Realme C55 6GB/128GB",
                        Slug = "realme-c55",
                        Description = "MediaTek Helio G88, 6.72 inch IPS LCD, 64MP Camera, 4G",
                        Image = "https://images.unsplash.com/photo-1591337676887-a217a6970a8a?w=800",
                        Category = smartphone,
                        Brand = realme,
                        Price = 4490000,
                        CapitalPrice = 3800000,
                        Quantity = 110,
                        SoldOut = 0
                    },

                    // PC DESKTOPS (4 products)
                    new ProductModel
                    {
                        Name = "PC Dell OptiPlex 7010 Tower",
                        Slug = "pc-dell-optiplex-7010-tower",
                        Description = "Intel Core i5-13500, 16GB RAM, 512GB SSD, Intel UHD Graphics 770",
                        Image = "https://images.unsplash.com/photo-1587202372616-b43abea06c2a?w=800",
                        Category = pc,
                        Brand = dell,
                        Price = 13990000,
                        CapitalPrice = 12500000,
                        Quantity = 35,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "PC HP ProDesk 400 G9 SFF",
                        Slug = "pc-hp-prodesk-400-g9-sff",
                        Description = "Intel Core i5-12500, 16GB RAM, 512GB SSD, Intel UHD Graphics 730",
                        Image = "https://images.unsplash.com/photo-1593640495253-23196b27a87f?w=800",
                        Category = pc,
                        Brand = hp,
                        Price = 15490000,
                        CapitalPrice = 14000000,
                        Quantity = 28,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "PC Lenovo ThinkCentre M70t Gen 3",
                        Slug = "pc-lenovo-thinkcentre-m70t-gen-3",
                        Description = "Intel Core i7-12700, 16GB RAM, 512GB SSD, Intel UHD Graphics 770",
                        Image = "https://images.unsplash.com/photo-1555680202-c5f074d4b2f8?w=800",
                        Category = pc,
                        Brand = lenovo,
                        Price = 16990000,
                        CapitalPrice = 15500000,
                        Quantity = 22,
                        SoldOut = 0
                    },
                    new ProductModel
                    {
                        Name = "PC ASUS ExpertCenter D7 Tower D700TD",
                        Slug = "pc-asus-expertcenter-d7-tower-d700td",
                        Description = "Intel Core i5-12400, 16GB RAM, 512GB SSD, Intel UHD Graphics 730",
                        Image = "https://images.unsplash.com/photo-1593640495253-23196b27a87f?w=800",
                        Category = pc,
                        Brand = asus,
                        Price = 14990000,
                        CapitalPrice = 13500000,
                        Quantity = 30,
                        SoldOut = 0
                    }
                );

                _context.SaveChanges();
            }

        }

        public static async Task CreateAdminAccount(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<AppUserModel>>();

            string[] roleNames = { "Admin", "User", "Seller" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminRole = await roleManager.FindByNameAsync("Admin");
            if (adminRole != null)
            {
                var adminUser = await userManager.FindByEmailAsync("admin@shopping.com");
                if (adminUser == null)
                {
                    var newAdminUser = new AppUserModel
                    {
                        UserName = "admin",
                        Email = "admin@shopping.com",
                        EmailConfirmed = true,
                        RoleId = adminRole.Id
                    };

                    var result = await userManager.CreateAsync(newAdminUser, "Admin@123");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newAdminUser, "Admin");
                        Console.WriteLine("Admin account created successfully!");
                        Console.WriteLine("Email: admin@shopping.com");
                        Console.WriteLine("Password: Admin@123");
                    }
                }
            }
        }
    }
}
