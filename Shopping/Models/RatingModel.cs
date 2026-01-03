using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Models
{
    public class RatingModel
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        [Required, MinLength(4, ErrorMessage = "Yêu Cầu Nhập Đánh giá Sản phẩm ")]
        public string Comment { get; set; }
        [Required, MinLength(4, ErrorMessage = "Yêu Cầu Nhập Tên Sản phẩm ")]
        public string Name { get; set; }
        [Required, MinLength(4, ErrorMessage = "Yêu Cầu Nhập Email Sản phẩm ")]
        public string Email { get; set; }
        public int Star { get; set; }
        [ForeignKey("ProductId")]
        public ProductModel Product { get; set; }

    }
}
