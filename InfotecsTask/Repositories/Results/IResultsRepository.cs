using InfotecsTask.Models;

namespace InfotecsTask.Repositories.Results
{
    public interface IResultsRepository
    {
        public Task CreateAsync(Models.Results result);
    }
}
