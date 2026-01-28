using InfotecsTask.Data;
using InfotecsTask.Queryies;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Models.Results>> GetFiltered(ResultsQuery query)
        {
            var q = _db_context.Results
                       .Include(r => r.File)
                       .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.FileName))
                q = q.Where(r => r.File.FileName == query.FileName);

            if (query.MinDateStart.HasValue)
                q = q.Where(r => r.MinDate >= query.MinDateStart);

            if (query.MinDateEnd.HasValue)
                q = q.Where(r => r.MinDate <= query.MinDateEnd);

            if (query.AvgValueStart.HasValue)
                q = q.Where(r => r.AvgValue >= query.AvgValueStart);

            if (query.AvgValueEnd.HasValue)
                q = q.Where(r => r.AvgValue <= query.AvgValueEnd);

            if (query.AvgExecutionTimeStart.HasValue)
                q = q.Where(r => r.AvgExecutionTime >= query.AvgExecutionTimeStart);

            if (query.AvgExecutionTimeEnd.HasValue)
                q = q.Where(r => r.AvgExecutionTime <= query.AvgExecutionTimeEnd);

            return await q.ToListAsync();
        }

        public async Task<List<Models.Results>> GetSorted(string file_name)
        {
            var query = _db_context.Results.Include(r => r.File)
                                            .Where(r => r.File.FileName == file_name)
                                            .OrderByDescending(r => r.MinDate)
                                            .Take(10);
            return await query.ToListAsync();
        }
    }
}
