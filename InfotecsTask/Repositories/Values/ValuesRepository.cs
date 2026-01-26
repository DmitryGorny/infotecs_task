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
            await using var transaction = await _db_context.Database.BeginTransactionAsync();
            try
            {
                await _db_context.BulkInsertAsync(values);
                await _db_context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
