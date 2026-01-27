namespace InfotecsTask.Models
{
    public class Files
    {   
        public int Id { get; set; }
        public string FileName { get; set; }

        public ICollection<Values> Values { get; set; } = new List<Values>();
    }
}
