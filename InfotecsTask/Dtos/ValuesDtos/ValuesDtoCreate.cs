using InfotecsTask.Dtos.ValuesDtos.Validators;
using System.ComponentModel.DataAnnotations;

namespace InfotecsTask.Dtos.ValuesDtos
{using Microsoft.AspNetCore.Mvc;
    public class ValuesDtoCreate
    {

        [DateValidation]
        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, MinimumIsExclusive = true)]
        public double ExecutionTime { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, MinimumIsExclusive = true)]
        public decimal Value { get; set; }
    }
}
