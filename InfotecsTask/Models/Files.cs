using System.Text.Json.Serialization;

namespace InfotecsTask.Models
{
    public class Files
    {   
        public int Id { get; set; }
        public string FileName { get; set; }

        [JsonIgnore]
        public ICollection<Values> Values { get; set; } = new List<Values>();
    }
}
