using InfotecsTask.Models;

namespace InfotecsTask.Repositories.ValuesRepository
{
    public interface IValuesRepository
    {
        public Task BulkCreateAsync(List<Values> values);

        public Task<List<Values>> GetSorted(string file_name);
    }
}
