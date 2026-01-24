using InfotecsTask.Dtos.ValuesDtos;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InfotecsTask.Services.ValuesService
{
    public interface IValuesService
    {
        public bool CreateValues(StreamReader reader, out List<string> errors);
    }

    }
