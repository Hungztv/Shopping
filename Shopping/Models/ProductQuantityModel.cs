using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class ProductQuantityModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Yêu Cầu Không bỏ trống số lượng sản phẩm")]
        public int Quantity { get; set; }
       
        public int ProductId { get; set; }

        public DateTime Dateceated { get; set; }
       
    }
}
