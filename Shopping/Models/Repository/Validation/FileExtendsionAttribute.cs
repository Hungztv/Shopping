using System.ComponentModel.DataAnnotations;

namespace Shopping.Models.Repository.Validation
{
    public class FileExtendsionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extendsion = Path.GetExtension(file.FileName);
                string[] extendsions = { ".jpg", ".jpeg", ".png", ".gif" };
                bool result = extendsions.Any(x => extendsion.EndsWith(x));
                if (!result)
                {
                    return new ValidationResult("File must be jpg, jpeg, png or gif");

                }
            }
            return ValidationResult.Success;
        }

    }
}
