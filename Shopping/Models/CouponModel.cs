using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class CouponModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code is required")]
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateExpired { get; set; }
       
      
        [Required(ErrorMessage = "Discount is required")]
        public int Quantity { get; set; }
        public int Status { get; set; }
    }
}
