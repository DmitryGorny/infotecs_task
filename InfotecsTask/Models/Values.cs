namespace InfotecsTask.Models
{
    public class Values
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public Files File { get; set; }
        public DateTime Date { get; set; }
        public double ExecutionTime { get; set; }
        public decimal Value { get; set; }
    }
}
