using InfotecsTask.Dtos.ResultsDto;
using InfotecsTask.Mappers;
using InfotecsTask.Models;
using InfotecsTask.Queryies;
using InfotecsTask.Repositories.Results;
using InfotecsTask.Repositories.ValuesRepository;
using InfotecsTask.Services.ValuesService;
using Results = InfotecsTask.Models.Results;

namespace InfotecsTask.Services.ResultsService
{
    public abstract class ResultsServiceBase : IResultsService
    {

        protected readonly IResultsRepository _resultsRepository;
        public ResultsServiceBase(IResultsRepository resultsRepo)
        {
            _resultsRepository = resultsRepo;
        }

        public async Task CreateResult(IReadOnlyList<Values> values)
        {
            double DeltaTimeSeconds = CalculateDeltaTimeSeconds(values);
            DateTime StartTime = GetStartTime(values);
            double ExecutionTime = CalculateAverageExecutionTime(values);
            decimal AverageValue = CalculateAverageValue(values);
            decimal MedianValue = CalculateMedianValue(values);
            decimal MaxValue = CalculateMaxValue(values);
            decimal MinValue = CalculateMinValue(values);

            ResultsCreateDto dto = new ResultsCreateDto 
            {
                FileId = values[0].FileId,
                DeltaTimeSeconds = DeltaTimeSeconds,
                MinDate = StartTime,
                AvgExecutionTime = ExecutionTime,
                AvgValue = AverageValue,
                MedianValue = MedianValue,
                MaxValue = MaxValue,
                MinValue = MinValue
            };

            Models.Results result = dto.ToResultsFromCreateDto();
            await _resultsRepository.CreateAsync(result);
        }

        public async Task<List<Results>> GetFilteredResults(ResultsQuery query)
        {
            var results = await _resultsRepository.GetFiltered(query);
            return results;
        }

        protected abstract double CalculateDeltaTimeSeconds(IReadOnlyList<Values> values);
        protected abstract DateTime GetStartTime(IReadOnlyList<Values> values);
        protected abstract double CalculateAverageExecutionTime(IReadOnlyList<Values> values);
        protected abstract decimal CalculateAverageValue(IReadOnlyList<Values> values);
        protected abstract decimal CalculateMedianValue(IReadOnlyList<Values> values);
        protected abstract decimal CalculateMaxValue(IReadOnlyList<Values> values);
        protected abstract decimal CalculateMinValue(IReadOnlyList<Values> values);
    }

    public class ResultsService : ResultsServiceBase, IResultsService
    {
       private readonly IResultsRepository _resultsRepository;

        public ResultsService(IResultsRepository resultsRepository) : base(resultsRepository) 
        {
            _resultsRepository = resultsRepository;
        }

        protected override double CalculateDeltaTimeSeconds(IReadOnlyList<Values> values)
        {
            if (!values.Any()) return 0;
            return (values.Max(v => v.Date) - values.Min(v => v.Date)).TotalSeconds;
        }

        protected override DateTime GetStartTime(IReadOnlyList<Values> values)
        {
            if (!values.Any()) throw new ArgumentException("Values list is empty");
            return values.Min(v => v.Date);
        }

        protected override double CalculateAverageExecutionTime(IReadOnlyList<Values> values)
        {
            if (!values.Any()) return 0;
            return values.Average(v => v.ExecutionTime);
        }

        protected override decimal CalculateAverageValue(IReadOnlyList<Values> values)
        {
            if (!values.Any()) return 0;
            return values.Average(v => v.Value);
        }

        protected override decimal CalculateMedianValue(IReadOnlyList<Values> values)
        {
            var sorted = values.Select(v => v.Value).OrderBy(v => v).ToList();
            int count = sorted.Count;
            if (count == 0) return 0;

            if (count % 2 == 0)
            {
                return (sorted[count / 2 - 1] + sorted[count / 2]) / 2;
            }
            else
            {
                return sorted[count / 2];
            }
        }

        protected override decimal CalculateMaxValue(IReadOnlyList<Values> values)
        {
            if (!values.Any()) return 0;
            return values.Max(v => v.Value);
        }

        protected override decimal CalculateMinValue(IReadOnlyList<Values> values)
        {
            if (!values.Any()) return 0;
            return values.Min(v => v.Value);
        }
    }
}
