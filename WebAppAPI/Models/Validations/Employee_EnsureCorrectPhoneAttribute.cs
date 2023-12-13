using System.ComponentModel.DataAnnotations;

namespace WebAppAPI.Models.Validations
{
    public class Employee_EnsureCorrectPhoneAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var employee = validationContext.ObjectInstance as Employee;
            if(employee != null) {
                int digitNumber = employee.Phone.Length;
                if (digitNumber <= 0) {
                    return new ValidationResult("phone number is required.");
                } else if (digitNumber < 10 || digitNumber > 10)
                {
                    return new ValidationResult("phone number should be equal to 10 digits.");
                } 
            }
            return ValidationResult.Success;
        }
    }
}
