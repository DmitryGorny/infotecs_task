using InfotecsTask.Dtos.FilesDto;
using InfotecsTask.Dtos.ResultsDto;
using InfotecsTask.Models;

namespace InfotecsTask.Mappers
{
    public static class FilesMapper
    {
        public static Files ToFilesFromCreateDto(this FilesCreateDto dto)
        {
            return new Files
            {
               FileName = dto.FileName,
            };
        }
    }
}
