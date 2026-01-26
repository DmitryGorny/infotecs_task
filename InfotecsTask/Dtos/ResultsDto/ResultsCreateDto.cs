using System.ComponentModel.DataAnnotations;

namespace InfotecsTask.Dtos.ResultsDto
{
    public class ResultsCreateDto
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public double DeltaTimeSeconds { get; set; }

        [Required]
        public DateTime MinDate { get; set; }

        [Required]
        public double AvgExecutionTime { get; set; }

        [Required]
        public decimal AvgValue { get; set; }

        [Required]
        public decimal MedianValue { get; set; }

        [Required]
        public decimal MaxValue { get; set; }

        [Required]
        public decimal MinValue { get; set; }
    }
}
