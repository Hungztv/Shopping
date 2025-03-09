using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Yêu Cầu Nhập Tên Danh Mục")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Yêu Cầu Nhập Mô tả  Danh Mục")]
        public string Description { get; set; }
        
        public string Slug { get; set; }
        public string Status { get; set; }
    }
}
