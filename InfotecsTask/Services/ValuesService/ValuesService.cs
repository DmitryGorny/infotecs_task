using InfotecsTask.Dtos.ValuesDtos;
using InfotecsTask.Mappers;
using InfotecsTask.Models;
using InfotecsTask.Repositories.Results;
using InfotecsTask.Repositories.ValuesRepository;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection.PortableExecutable;

namespace InfotecsTask.Services.ValuesService
{
    public abstract class ValuesServiceBase : IValuesService
    {

        protected readonly IValuesRepository _ValueRepository;
        private List<Values> _values { get; set; }

        public IReadOnlyList<Values> GetValues() {
           return _values.AsReadOnly();
        }

        public ValuesServiceBase(IValuesRepository valueRepository)
        {
            _ValueRepository = valueRepository;
        }

        public async Task<List<Values>> GetSortedValues(string file_name)
        {
            var results = await _ValueRepository.GetSorted(file_name);
            return results;
        }

        public async Task<List<string>> CreateValues(StreamReader reader, int file_id)
        {
            List<string> errors = new List<string>();
            string line;
            int line_number = 0;
            _values = new List<Values>();

            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (line_number == 0)
                {
                    line_number++;
                    continue;
                }

                if (line_number >= 10000)
                {
                    errors.Add("Ошибка: Файл содержит больше чем 10000 строк");
                    break;
                }
                string[] line_arr = line.Split(";");
                List<string> current_errors = new List<string>();
                ValuesDtoCreate? dto = ParseLine(line_arr, file_id, out current_errors);

                if (dto == null)
                {
                    errors.AddRange(current_errors);
                    continue;
                }

                var context = new ValidationContext(dto);
                var results = new List<ValidationResult>();

                bool isValid = Validator.TryValidateObject(dto, context, results, true);

                if (!isValid)
                {
                    foreach (var r in results)
                    {
                        errors.Add($"Строка: {line_number}, {r.ErrorMessage}");
                    }
                }

                Values values_from_dto = dto.ToValuesFromCreateDto();
                _values.Add(values_from_dto);
                line_number++;
            }
            if (errors.Count > 0)
            {
                return errors;
            }
  
            await SaveToDB(_values);
            return errors;
        }
        protected abstract ValuesDtoCreate? ParseLine(string[] line, int file_id, out List<string> errors);

        protected abstract Task SaveToDB(List<Values> values);

        protected abstract DateTime? ParseDate(string date, out string? error);

        protected abstract double? ParceExecutionTime(string execution_time, out string? error);

        protected abstract decimal? ParceValue(string value, out string? error);

    }

    public class ValuesService : ValuesServiceBase, IValuesService
    {

        public ValuesService(IValuesRepository repository) : base(repository)
        { 
        
        }

        protected override ValuesDtoCreate? ParseLine(string[] line, int file_id, out List<string> errors)
        {
            errors = new List<string>();

            DateTime? date = ParseDate(line[0], out string? date_error);
            double? execution_time = ParceExecutionTime(line[1], out string? execution_time_error);
            decimal? value = ParceValue(line[2], out string? value_error);

            bool is_null = false;

            if (date == null) {
                errors.Add(date_error!);
                is_null = true;
            }

            if (execution_time == null)
            {
                errors.Add(execution_time_error!);
                is_null = true;
            }

            if (value == null)
            {
                errors.Add(value_error!);
                is_null = true;
            }

            if (is_null)
            {
                return null;
            }
            return new ValuesDtoCreate { 
                Date = date!.Value, 
                ExecutionTime = execution_time!.Value, 
                Value = value!.Value,
                FileId = file_id,    
            };
            
            
        }

        protected override DateTime? ParseDate(string date, out string? error) 
        {
            if (date == null)
            {
                error = "Отсутсвует значение в файле";
                return null;
            }


            bool success = DateTime.TryParseExact(
                        date,
                        "yyyy-MM-dd'T'HH:mm:ss.ffff'Z'",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                        out DateTime parsed_date);

                if (!success)
                {
                    error = "Некорректный формат даты";
                    return null;
                }
            error = null;
            return parsed_date;
        }

        protected override double? ParceExecutionTime(string execution_time, out string? error)
        {
            error = null;
            execution_time = execution_time .Replace(',', '.');
            bool was_parsed = double.TryParse(execution_time,
                                            NumberStyles.Number,
                                            CultureInfo.InvariantCulture,
                                            out double result);

            if (!was_parsed)
            {
                error = "Неккоректный формат времени выполнения";
                return null;
            }
            return result;
        }

        protected override decimal? ParceValue(string value, out string? error)
        {
            error = null;
            value = value.Replace(',', '.');
            bool was_parsed = decimal.TryParse(value, 
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out decimal result);

            if (!was_parsed)
            {
                error = "Неккоректный формат показателя";
                return null;
            }
            return result;
        }

        protected override async Task SaveToDB(List<Values> values) 
        {
            await _ValueRepository.BulkCreateAsync(values);
        }
    }
}

        