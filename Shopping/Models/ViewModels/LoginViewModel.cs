using System.ComponentModel.DataAnnotations;

namespace Shopping.Models.ViewModels
{
    public class LoginViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string UserName { get; set; }
    
        [DataType(DataType.Password), Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
