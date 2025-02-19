using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class BrandModel
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(4, ErrorMessage = "Yêu Cầu Nhập Tên Thương Hiệu")]
        public string Name { get; set; }
        [Required, MinLength(4, ErrorMessage = "Yêu Cầu Nhập Mô tả Thương Hiệu")]
        public string Description { get;set; }
        
        public string Slug { get; set; }
        public string Status { get; set; }
    }
}
