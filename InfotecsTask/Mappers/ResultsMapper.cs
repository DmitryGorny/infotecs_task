using InfotecsTask.Dtos.ResultsDto;
using System.Runtime.CompilerServices;

namespace InfotecsTask.Mappers
{
    public static class ResultsMapper
    {
        public static ResultsDtoGet ToDtoFromReuslts(this Models.Results result)
        {
            return new ResultsDtoGet
            {
                DeltaTimeSeconds = result.DeltaTimeSeconds,
                MinDate = result.MinDate,
                AvgExecutionTime = result.AvgExecutionTime,
                AvgValue = result.AvgValue,
                MedianValue = result.MedianValue,
                MaxValue = result.MaxValue,
                MinValue = result.MinValue,
            };
        }
        public static Models.Results ToResultsFromCreateDto(this ResultsCreateDto dto)
        {
            return new Models.Results
            {
                FileId = dto.FileId,
                DeltaTimeSeconds = dto.DeltaTimeSeconds,
                MinDate = dto.MinDate,
                AvgExecutionTime = dto.AvgExecutionTime,
                AvgValue = dto.AvgValue,
                MedianValue = dto.MedianValue,
                MaxValue = dto.MaxValue,
                MinValue = dto.MinValue,
            };
        }
    }
}
