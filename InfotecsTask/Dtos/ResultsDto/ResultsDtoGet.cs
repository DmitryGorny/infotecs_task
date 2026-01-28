using System.ComponentModel.DataAnnotations;

namespace InfotecsTask.Dtos.ResultsDto
{
    public class ResultsDtoGet
    {
        public double DeltaTimeSeconds { get; set; }

        public DateTime MinDate { get; set; }

        public double AvgExecutionTime { get; set; }

        public decimal AvgValue { get; set; }

        public decimal MedianValue { get; set; }

        public decimal MaxValue { get; set; }
        public decimal MinValue { get; set; }
    }
}
