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
                CategoryModel Macbook = new CategoryModel {   Name = "Macbook", Slug = "Macbook", Description = "Macbook is the new", Status = "Active" };
                CategoryModel pc = new CategoryModel {Name = "PC", Slug = "PC", Description = "PC is the new", Status = "Active" };
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
                    }
                );
                _context.SaveChanges();
            }
        }
        
    }
}
