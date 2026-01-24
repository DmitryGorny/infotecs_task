using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace InfotecsTask.Dtos.ValuesDtos.Validators
{
    public class DateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value is not DateTime givenDate)
                return new ValidationResult("Некорректный тип значения даты");

            DateTime minDate = new DateTime(2000, 1, 1);
            DateTime currentDate = DateTime.Now.Date;
            if (givenDate < minDate || givenDate > currentDate) return new ValidationResult("Неккоректный промежуток даты");
        

            return ValidationResult.Success;
        }
    }
}
