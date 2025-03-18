using System.ComponentModel.DataAnnotations;

namespace Shopping.Models.ViewModels
{
    public class ProductDetailsViewModel
    {
        public ProductModel ProductDetails { get; set; }
        [Required(ErrorMessage = "Please enter your comment")]
        public string Comment { get; set; }
        [Required(ErrorMessage = "Please enter your star")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter your name")]
        public string Email { get; set; }
    }
}
