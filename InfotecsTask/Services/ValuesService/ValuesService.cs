using InfotecsTask.Dtos.ValuesDtos;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection.PortableExecutable;

namespace InfotecsTask.Services.ValuesService
{
    public abstract class ValuesServiceBase : IValuesService
    {
        public bool CreateValues(StreamReader reader, out List<string> errors)
        {
            errors = new List<string>();
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] line_arr = line.Split(";");
                List<string> current_errors = new List<string>();
                ValuesDtoCreate? dto = ParseLine(line_arr, out current_errors);

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
                        errors.Add($"{r.ErrorMessage}");
                    }
                }
            }
            if (errors.Count > 0) 
            {
                return false;
            }
            return true;
        }
        protected abstract ValuesDtoCreate? ParseLine(string[] line, out List<string> errors);

        protected abstract void SaveToDB(List<ValuesDtoCreate> dtos);

        protected abstract DateTime? ParseDate(string date, out string? error);

        protected abstract double? ParceExecutionTime(string execution_time, out string? error);

        protected abstract decimal? ParceValue(string value, out string? error);
    }

    public class ValuesService : ValuesServiceBase, IValuesService
    {

        protected override ValuesDtoCreate? ParseLine(string[] line, out List<string> errors)
        {
            errors = new List<string>();

            DateTime? date = ParseDate(line[0], out string? date_error);
            double? execution_time = ParceExecutionTime(line[1], out string? execution_time_error);
            decimal? value = ParceValue(line[2], out string? value_error);

            bool is_null = false;

            if (date == null) {
                errors.Add(date_error);
                is_null = true;
            }

            if (execution_time == null)
            {
                errors.Add(execution_time_error);
                is_null = true;
            }

            if (value == null)
            {
                errors.Add(value_error);
                is_null = true;
            }

            if (is_null)
            {
                return null;
            }
            return new ValuesDtoCreate { Date = date!.Value, ExecutionTime = execution_time!.Value, Value = value!.Value };
            
            
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

            bool was_parsed = double.TryParse(execution_time, out double result);

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

            bool was_parsed = decimal.TryParse(value, out decimal result);

            if (!was_parsed)
            {
                error = "Неккоректный формат показателя";
                return null;
            }
            return result;
        }

        protected override void SaveToDB(List<ValuesDtoCreate> dtos) { }
    }
}

        