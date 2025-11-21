using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class CouponModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateExpired { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        public int Status { get; set; }
        // New fields for discount logic
        // DiscountValue: if IsPercent = true => percentage (0-100), else fixed amount in VND
        [Range(0, 100000000, ErrorMessage = "Giá trị giảm không hợp lệ")]
        public decimal DiscountValue { get; set; }
        public bool IsPercent { get; set; }
    }
}
