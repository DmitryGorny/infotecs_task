namespace InfotecsTask.Models
{
    public class Results
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public double DeltaTimeSeconds { get; set; }
        public DateTime MinDate { get; set; }
        public double AvgExecutionTime { get; set; }
        public decimal AvgValue { get; set; }
        public decimal MedianValue { get; set; }
        public decimal MaxValue { get; set; }
        public decimal MinValue { get; set; }
    }
}
