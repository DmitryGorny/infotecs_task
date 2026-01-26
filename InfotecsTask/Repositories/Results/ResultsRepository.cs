using InfotecsTask.Data;

namespace InfotecsTask.Repositories.Results
{
    public class ResultsRepository : IResultsRepository
    {
        private readonly AppDBContext _db_context;
        public ResultsRepository(AppDBContext context)
        {
            _db_context = context;
        }
        public async Task CreateAsync(Models.Results result)
        {
            await _db_context.Results.AddAsync(result);
        }
    }
}
