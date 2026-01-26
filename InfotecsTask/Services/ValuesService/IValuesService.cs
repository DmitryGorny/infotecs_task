using InfotecsTask.Dtos.ValuesDtos;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InfotecsTask.Services.ValuesService
{
    public interface IValuesService
    {
        public Task<List<string>> CreateValues(StreamReader reader);
    }

    }
