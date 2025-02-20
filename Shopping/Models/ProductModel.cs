using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        [Required,MinLength(4,ErrorMessage ="Yêu Cầu Nhập Tên Sản phẩm ")]
        public string Name { get; set; }
        
        public string Slug { get; set; }
        [Required, MinLength(4, ErrorMessage = "Yêu Cầu Nhập Mô tả Sản phẩm ")]
        public string Description { get; set; }
        public string Price { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
        public BrandModel Brand { get; set; }   
        public String Image { get; set; }
    }
}
