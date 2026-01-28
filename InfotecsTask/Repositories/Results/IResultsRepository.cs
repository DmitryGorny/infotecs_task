using InfotecsTask.Models;
using InfotecsTask.Queryies;

namespace InfotecsTask.Repositories.Results
{
    public interface IResultsRepository
    {
        public Task CreateAsync(Models.Results result);

        public Task<List<Models.Results>> GetFiltered(ResultsQuery query);
        
    }
}
