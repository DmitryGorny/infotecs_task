using InfotecsTask.Dtos.ResultsDto;
using InfotecsTask.Models;
using InfotecsTask.Queryies;
using System.Collections.Generic;
using Results = InfotecsTask.Models.Results;

namespace InfotecsTask.Services.ResultsService
{
    public interface IResultsService
    {
        public Task CreateResult(IReadOnlyList<Values> values);

        public Task<List<Results>> GetFilteredResults(ResultsQuery query);
        public Task<List<Results>> GetSortedResults(string file_name);
    }
}
