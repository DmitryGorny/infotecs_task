using System.ComponentModel.DataAnnotations;

namespace InfotecsTask.Dtos.FilesDto
{
    public class FilesCreateDto
    {
        
        [Required]
        public string FileName { get; set; }
    }
}
