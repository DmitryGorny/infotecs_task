using InfotecsTask.Models;

namespace InfotecsTask.Repositories.ValuesRepository
{
    public interface IValuesRepository
    {
        public Task BulkCreateAsync(List<Values> values);
    }
}
