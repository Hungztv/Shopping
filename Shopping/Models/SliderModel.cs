using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Models
{
    public class SliderModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }
        
        public string Image { get; set; }
        [Required(ErrorMessage = "Please enter description")]
        public string Description { get; set; }
        public int? Status { get; set; }
        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
    }
}
