using InfotecsTask.Dtos.ResultsDto;

namespace InfotecsTask.Mappers
{
    public static class ResultsMapper
    {
        public static Models.Results ToResultsFromCreateDto(this ResultsCreateDto dto)
        {
            return new Models.Results
            {
                FileName = dto.FileName,
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
