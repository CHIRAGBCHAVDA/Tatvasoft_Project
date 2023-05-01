using System.ComponentModel.DataAnnotations;

namespace CIPlatform
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class DateGreaterThanAttribute: ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (comparisonProperty == null)
            {
                return new ValidationResult($"Property {_comparisonProperty} not found.");
            }

            var comparisonValue = comparisonProperty.GetValue(validationContext.ObjectInstance, null);

            if (!(value is DateTime) || !(comparisonValue is DateTime))
            {
                return ValidationResult.Success;
            }

            if ((DateTime)value <= (DateTime)comparisonValue)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
