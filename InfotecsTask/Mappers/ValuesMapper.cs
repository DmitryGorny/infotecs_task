using InfotecsTask.Controllers;
using InfotecsTask.Dtos.ValuesDtos;
using InfotecsTask.Models;

namespace InfotecsTask.Mappers
{
    public static class ValuesMapper
    {
        public static ValuesDtoGet ToDtoFromValues(this Values value)
        {
            return new ValuesDtoGet
            {
                FileName = value.File.FileName,
                Date = value.Date,
                ExecutionTime = value.ExecutionTime,
                Value = value.Value,
            };
        }
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
