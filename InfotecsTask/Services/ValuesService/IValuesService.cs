using InfotecsTask.Dtos.ValuesDtos;
using InfotecsTask.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InfotecsTask.Services.ValuesService
{
    public interface IValuesService
    {
        public Task<List<string>> CreateValues(StreamReader reader, int file_id);

        public IReadOnlyList<Values> GetValues();
    }

    }
