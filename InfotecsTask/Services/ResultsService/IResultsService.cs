using InfotecsTask.Dtos.ResultsDto;
using InfotecsTask.Models;
using System.Collections.Generic;

namespace InfotecsTask.Services.ResultsService
{
    public interface IResultsService
    {
        public Task CreateResult(IReadOnlyList<Values> values);
    }
}
