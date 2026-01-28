using InfotecsTask.Dtos.ValuesDtos;
using InfotecsTask.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InfotecsTask.Services.ValuesService
{
    public interface IValuesService
    {
        public Task<List<string>> CreateValues(StreamReader reader);

        public Task<List<Values>> GetSortedValues(string file_name);

        public Task<IReadOnlyList<Values>> AddValuesToDB(int fileId);
    }

}
