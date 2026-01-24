using InfotecsTask.Dtos.ValuesDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InfotecsTask.Controllers
{
    [Route("api/values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ValuesController() { }

        [HttpPost]
        public IActionResult FileReader(IFormFile csv_file)
        {
            if (csv_file == null || csv_file.Length < 0) return BadRequest("Файл не выбран");

            using (var reader = new StreamReader(csv_file.OpenReadStream(), Encoding.UTF8))
            {
                string line;
                int line_number = 0;
                var errors = new List<string>();

                while ((line = reader.ReadLine()) != null)                 
                {
                    line_number++;
                    string[] line_arr = line.Split(";");
                    var dto = new ValuesDtoCreate { Date = line_arr[0] };

                    var context = new ValidationContext(dto);
                    var results = new List<ValidationResult>();

                    bool isValid = Validator.TryValidateObject(dto, context, results, true);

                    if (!isValid)
                    {
                        foreach (var r in results)
                        {
                            errors.Add($"Строка {line_number}: {r.ErrorMessage}");
                        }
                    }

                }
                if (errors.Any())
                {
                    return BadRequest(errors);
                }
            }

            return Ok("Файл записан в базу");
        }

    }
}
