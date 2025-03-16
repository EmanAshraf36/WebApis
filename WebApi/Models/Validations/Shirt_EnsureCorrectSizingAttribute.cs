using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Validations;

public class Shirt_EnsureCorrectSizingAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var shirt = validationContext.ObjectInstance as Shirt;
        if (shirt != null && !string.IsNullOrWhiteSpace(shirt.Gender))
        {
            if (shirt.Gender.Equals("men", StringComparison.OrdinalIgnoreCase) && shirt.Size < 8)
            {
                return new ValidationResult("Shirt must be at least of size 8 for men");
            }
            else if(shirt.Gender.Equals("women", StringComparison.OrdinalIgnoreCase) && shirt.Size < 6)
            {
                return new ValidationResult("Shirt must be at least of size 6 for women");
            }
        }
        return ValidationResult.Success;
    }
}