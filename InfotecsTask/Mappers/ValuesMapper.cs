using InfotecsTask.Controllers;
using InfotecsTask.Dtos.ValuesDtos;
using InfotecsTask.Models;

namespace InfotecsTask.Mappers
{
    public static class ValuesMapper
    {
        public static Values ToValuesFromCreateDto(this ValuesDtoCreate dto)
        {
            return new Values
            {
                Date = dto.Date,
                ExecutionTime = dto.ExecutionTime,
                Value = dto.Value,
                FileId = dto.FileId,
            };
        } 
    }
}
