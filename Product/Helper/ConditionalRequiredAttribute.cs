using Product.Models;
using System.ComponentModel.DataAnnotations;

namespace Product.Helper
{



   

    public class ConditionalRequiredAttribute : ValidationAttribute
    {
        private readonly string _statusPropertyName;
        private readonly Status _statusValue;

        public ConditionalRequiredAttribute(string statusPropertyName, Status statusValue)
        {
            _statusPropertyName = statusPropertyName;
            _statusValue = statusValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Get the status property from the model
            var statusProperty = validationContext.ObjectType.GetProperty(_statusPropertyName);
            if (statusProperty == null)
            {
                return new ValidationResult($"Unknown property: {_statusPropertyName}");
            }

            // Get the value of the status property
            var statusValue = (Status)statusProperty.GetValue(validationContext.ObjectInstance);

            // Validate based on the status value
            if (statusValue == _statusValue && value == null)
            {
                return new ValidationResult(ErrorMessage ?? "Field is required.");
            }

            return ValidationResult.Success;
        }
    }


}
