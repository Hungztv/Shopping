
using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class UserModel 
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is required"),EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password),Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }


    }
}
