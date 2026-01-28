using InfotecsTask.Data;
using InfotecsTask.Models;
using Microsoft.EntityFrameworkCore;
using EFCore.BulkExtensions;

namespace InfotecsTask.Repositories.ValuesRepository
{
    public class ValuesRepository : IValuesRepository
    {
        private readonly AppDBContext _db_context; 
        public ValuesRepository(AppDBContext context) 
        {
            _db_context = context;
        }

        public async Task BulkCreateAsync(List<Values> values)
        {
            await _db_context.BulkInsertAsync(values);
        }

        public async Task<List<Values>> GetSorted(string file_name)
        {
            var query = _db_context.Values.Include(r => r.File)
                                            .Where(r => r.File.FileName == file_name)
                                            .OrderByDescending(r => r.Date)
                                            .Take(10);
            return await query.ToListAsync();
        }
    }
}
