namespace InfotecsTask.Queryies
{
    public class ResultsQuery
    {
        public string? FileName { get; set; } = null;
        public DateTime? MinDateStart { get; set; } = null;
        public DateTime? MinDateEnd { get; set; } = null;
        public decimal? AvgValueStart { get; set; } = null;
        public decimal? AvgValueEnd { get; set; }
        public double? AvgExecutionTimeStart { get; set; } = null;
        public double? AvgExecutionTimeEnd { get; set; }
    }
}
