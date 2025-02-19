using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class CategoryModel
    {
        [Key]
        public string Id { get; set; }
        [Required,MinLength(4,ErrorMessage ="Yêu Cầu Nhập Tên Danh Mục")]
        public string Name { get; set; }
        [Required, MinLength(4, ErrorMessage = "Yêu Cầu Nhập Mô tả  Danh Mục")]
        public string Description { get; set; }
        
        public string Slug { get; set; }
        public string Status { get; set; }
    }
}
