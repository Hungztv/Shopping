using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Models
{
    public class ContactModel
    {
        [Key]
        [Required(ErrorMessage = "Yêu Cầu Nhập Tiêu Đề Website")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Yêu Cầu Nhập Bản Đồ")]
        public string Map { get; set; }
        [Required(ErrorMessage = "Yêu Cầu Nhập Email Liên Hệ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Yêu Cầu Nhập Phone")]

        public string Phone { get; set; }
        
        [Required(ErrorMessage = "Yêu Cầu Nhập Thông Tin Liên Hệ")]
        public string Description { get; set; }

        public string LogoImg { get; set; }
        [NotMapped]
        [FileExtension]
        public IFormFile? ImageUpload { get; set; }
    }
}
